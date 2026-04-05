using System.Configuration;

namespace AutoCompleteForm
{
    public class ConstantString
    {
        public static readonly string MainFolder =
            ConfigurationManager.AppSettings["MainFolder"]
            ?? @"C:\Users\ArcayosR\source\repos\1AutoCompleteForm\";

        public static readonly string IpmFormFolder =
            ConfigurationManager.AppSettings["IpmFormFolder"]
            ?? @"C:\Users\ArcayosR\source\repos\1AutoCompleteForm\IpmForm\";

        public static readonly string AcceptanceFormFolder =
            ConfigurationManager.AppSettings["AcceptanceFormFolder"]
            ?? @"C:\Users\ArcayosR\source\repos\1AutoCompleteForm\AcceptanceForm\";

        public static readonly string IpmFormCompletedFolder =
            ConfigurationManager.AppSettings["IpmFormCompletedFolder"]
            ?? @"C:\Users\ArcayosR\source\repos\1AutoCompleteForm\CompletedForms\";
    }
}
