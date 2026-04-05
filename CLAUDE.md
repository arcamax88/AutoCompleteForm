# CLAUDE.md — AutoCompleteForm

This file provides guidance for AI assistants working in this repository.

---

## Project Overview

**AutoCompleteForm** is a C# console application (.NET Framework 4.6.1) designed to automate
the generation of medical equipment test forms (Word `.docx` files). It reads work orders
from an XML export, matches each work order's equipment model against a local model database,
then produces completed IPM (Inspection/Preventive Maintenance) or Acceptance Test forms
with the correct parameters pre-filled.

The tool is used by a biomedical engineering department to reduce manual data entry when
processing equipment inspection work orders exported from the AIMS hospital management system.

---

## Repository Structure

```
AutoCompleteForm/
├── AutoCompleteForm.sln                   # Visual Studio 2015 solution
└── AutoCompleteForm/                      # Single C# project
    ├── AutoCompleteForm.csproj            # MSBuild project (TargetFramework: net461)
    ├── App.config                         # Runtime: .NET 4.6.1
    ├── Program.cs                         # Entry point — orchestrates the full workflow
    ├── ConstantString.cs                  # Hardcoded filesystem paths (Windows-specific)
    ├── model.cs                           # Model data class (equipment model properties)
    ├── Models.cs                          # XML serialization wrapper for a list of Model
    ├── ReadXmlFile.cs                     # Deserializes work orders and models from XML
    ├── ReadTesters.cs                     # Loads testers list via MedicalEquipmentTester.dll
    ├── SelectInputAndOutFilenames.cs      # Picks form templates and output paths by action type
    ├── ProcessWorkOrderOfLoanUnit.cs      # Handles loan-unit work orders (TAG_NUMBER = "NOEQU")
    ├── ReadOrReplaceMeasuredValue.cs      # Parses/transforms the Action field measured values
    ├── WriteUnfinishedWorkOrders.cs       # Serializes unmatched work orders back to XML
    └── Properties/AssemblyInfo.cs
```

---

## Key Data Flow (Program.cs)

```
1. Read XML files
   ├── List_of_Currently_Used_Tester.xml  → List<Tester>
   ├── Work Orders.xml                    → List<Work_Orders>  (AIMS export)
   └── models.xml                         → List<Model>

2. For each work order:
   ├── TAG_NUMBER != "NOEQU"  → registered unit path
   │   ├── Match wo.ModelName against models list (exact string match after Trim)
   │   ├── SelectInputAndOutFiles.Action()  → picks template + output .docx paths
   │   ├── wo.CreateIpmForm(inputPath, outputPath, testers)  → writes completed form
   │   └── No match → add to unfinished list
   └── TAG_NUMBER == "NOEQU"  → loan unit path
       └── ProcessWorkOrderOfLoanUnit.Start()  → parses MANUFACTURER/MODEL/SN from Action field

3. Write unmatched work orders back to Work Orders.xml
```

---

## Source File Reference

| File | Responsibility |
|------|---------------|
| `Program.cs` | Main loop; wires all components together |
| `ConstantString.cs` | Four folder path constants (see Paths section below) |
| `model.cs` | `Model` class: `EquipmentType`, `ModelNumber`, `ModelName`, `ConstantParameters`, `IpmForm`, `AcceptanceForm` |
| `Models.cs` | `[XmlRoot("Models")]` wrapper holding `List<Model>` for XML deserialization |
| `ReadXmlFile.cs` | `ReadXmlFile.WorkOrders(path)` and `ReadXmlFile.Models(path)` — standard `XmlSerializer` |
| `ReadTesters.cs` | Thin wrapper around `MedicalEquipmentTester.dll` |
| `SelectInputAndOutFilenames.cs` | Switch on `Action` prefix: `"INSPECTION FUNCTION & SAFETY A"`, `"INSPECTION FUNCTION & SAFETY A - I02"`, `"ACCEPTANCE TEST/COMMISSION"`. Merges work-order parameters with model defaults via `Dict1AndDict2.dll`. |
| `ProcessWorkOrderOfLoanUnit.cs` | Parses key=value pairs after `:` in the `Action` field; sets `MANUFACTURER`, `MODEL`, `SN`; sets `ControlNumber = "LOAN UNIT"` |
| `ReadOrReplaceMeasuredValue.cs` | Currently **commented-out** in `Program.cs`; splits the `Action` CSV and handles BTY prefix edge case |
| `WriteUnfinishedWorkOrders.cs` | Serializes `AIMSExport` (wrapper around `List<Work_Orders>`) back to the source XML path |

---

## External DLL Dependencies

The project references three custom DLLs that must be compiled from sibling repositories
(expected at `../../<repo-name>/bin/Debug/` relative to this project):

| DLL | Expected sibling repo | Key types used |
|-----|-----------------------|----------------|
| `WorkOrder.dll` | `WorkOrder` | `WorkOrder.WorkOrder`, `Work_Orders`, `AIMSExport` |
| `MedicalEquipmentTester.dll` | `MedicalEquipmentTester` | `Tester` |
| `Dict1AndDict2.dll` | `Dict1AndDict2` | `Dict1AndDict2.ConvertStringToDict()`, `.MergeExclusively()` |

These DLLs are checked into `bin/Debug/` of this repo so the project can build standalone,
but any changes to those libraries require recompiling and copying the DLLs here.

---

## Filesystem Paths (ConstantString.cs)

All paths are **hardcoded** to a specific Windows user account and must be updated if the
project is moved or run on a different machine:

```csharp
MainFolder             = @"C:\Users\ArcayosR\source\repos\1AutoCompleteForm\"
IpmFormFolder          = @"C:\Users\ArcayosR\source\repos\1AutoCompleteForm\IpmForm\"
AcceptanceFormFolder   = @"C:\Users\ArcayosR\source\repos\1AutoCompleteForm\AcceptanceForm\"
IpmFormCompletedFolder = @"C:\Users\ArcayosR\source\repos\1AutoCompleteForm\CompletedForms\"
```

**When assisting with path-related changes**, update `ConstantString.cs`. Do not scatter
literal paths elsewhere in the codebase.

---

## Action Field Format

The `Action` field on a work order drives form selection. It follows this pattern:

```
<ACTION_TYPE>[: <key=value,key=value,...>]
```

**Known action type prefixes** (matched by `SelectInputAndOutFilenames.cs`):
- `INSPECTION FUNCTION & SAFETY A` — standard IPM form
- `INSPECTION FUNCTION & SAFETY A - I02` — IPM form variant
- `ACCEPTANCE TEST/COMMISSION` — acceptance form

**Parameter merging:** If a colon-separated parameter string is present, it is merged with
the model's `ConstantParameters` via `Dict1AndDict2.MergeExclusively()` — the work order's
values take precedence over model defaults. Result is serialized back as CSV `key=value,`.

**Loan unit action format:**
```
<ANY_ACTION_TYPE>: MANUFACTURER=<val>, MODEL=<val>, SN=<val>
```

---

## Build & Run

### Requirements
- Windows machine (hardcoded paths, `.NET Framework 4.6.1`)
- Visual Studio 2015 or later (or MSBuild with .NET 4.6.1 targeting pack)

### Build
Open `AutoCompleteForm.sln` in Visual Studio and build (F6), or:
```
msbuild AutoCompleteForm.sln /p:Configuration=Debug
```
Output: `AutoCompleteForm/bin/Debug/AutoCompleteForm.exe`

### Run
Place the following files in `MainFolder` before running:
- `Work Orders.xml` — AIMS-exported work orders
- `models.xml` — equipment model database
- `List_of_Currently_Used_Tester.xml` — active testers

Then run the `.exe`. It will:
1. Print each work order to the console (pre- and post-process)
2. Generate `.docx` files in `IpmFormCompletedFolder`
3. Pause (`Console.ReadLine()`) waiting for Enter before writing the unfinished list
4. Overwrite `Work Orders.xml` with only the unmatched work orders

### No automated tests
There is no test project. Verification is done manually by inspecting console output and
the generated `.docx` files.

---

## Naming Conventions

- **Classes:** PascalCase, one public class per file, filename matches class name
- **Methods:** PascalCase; entry-point static methods are conventionally named `Start()` or `Action()`
- **Private fields:** `_camelCase` backing fields with public PascalCase properties (see `model.cs`)
- **Constants:** All-caps `string` constants in `ConstantString` (no `const` enum pattern)
- **Namespace:** Single namespace `AutoCompleteForm` across all files

---

## Conventions for AI Assistance

- **Do not introduce new path strings** outside `ConstantString.cs`.
- **Do not refactor** `ConstantString` to use `Path.Combine` unless explicitly asked — the
  project is stable legacy code.
- **Model matching is exact**: `wo.ModelName == model.ModelNumber.TrimStart().TrimEnd()`.
  New models must be added to `models.xml`, not hardcoded in C#.
- **`ReadOrReplaceMeasuredValue`** is intentionally commented out in `Program.cs:67`; do not
  uncomment it without understanding the effect on the `Action` field downstream.
- **Loan units** always have `TAG_NUMBER == "NOEQU"` and carry all equipment details in the
  colon-suffix of the `Action` field.
- When adding a new action type, add a `case` to the `switch` in
  `SelectInputAndOutFilenames.cs:27` following the existing pattern.
- The project targets **.NET Framework 4.6.1** — do not use C# 8+ features or .NET Core / .NET 5+ APIs.
