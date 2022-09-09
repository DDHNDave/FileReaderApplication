using FileReader.Interfaces;
using FileReader.Utilities;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileReader
{
    public class ScanFiles
    {
        protected readonly IFileScanner FileScanner;

        protected readonly FileMatchingResultMessagePrinter MessagePrinter;

        public ScanFiles(IFileScanner fileScanner, FileMatchingResultMessagePrinter messagePrinter)
        {
            this.FileScanner = fileScanner;
            this.MessagePrinter = messagePrinter;
        }

        protected string[] CleanUpPathCommandLineArguments(string[] arguments)
        {
            Regex normalizePathDividerExpression = new Regex(@"[\\\/]+", RegexOptions.IgnoreCase);
            Regex removePathNameExpression = new Regex(@"(\s{0,}\w{0,}path\w{0,}\s{0,}\=\s{0,})", RegexOptions.IgnoreCase);
            string result = string.Empty;

            foreach (string path in arguments)
                result += $"{normalizePathDividerExpression.Replace(removePathNameExpression.Replace(path, string.Empty), "-DIRECTORY-")}#";

            return result.Split("#")
                .Where(path => !string.IsNullOrEmpty(path))
                .Select(filePath => this.StandardizeFilePath(filePath))
                .ToArray(); 
        }

        public void Run(string[] arguments)
        {
            var cleanedUpCommandLineArguments = this.CleanUpPathCommandLineArguments(arguments);
            if (cleanedUpCommandLineArguments == null || (cleanedUpCommandLineArguments?.Length ?? 0) == 0)
            {
                Console.WriteLine("No filepath or filePaths have been specified");
                this.StartInterActiveConsole();
            }

            switch ((cleanedUpCommandLineArguments?.Length ?? 0))
            {
                case 0:
                    Console.WriteLine("No filepath or filePaths have been specified");
                    this.StartInterActiveConsole();
                    break;

                case 1:
                    this.MessagePrinter.PrintFilesMatchingSearchTermsMessage(this.FileScanner.ScanFileForSearchTerm(cleanedUpCommandLineArguments[0]));
                    break;

                default:
                    if((cleanedUpCommandLineArguments?.Length ?? 0) > 1)
                        this.MessagePrinter.PrintFilesMatchingSearchTermsMessage(this.FileScanner.ScanFilesForSearchTerm(cleanedUpCommandLineArguments));
                    break;
            }
        }
        protected string StandardizeFilePath(string attemptedPath)
        {
            string normalizedPath = string.Empty;
            foreach (string part in attemptedPath.Split("-DIRECTORY-"))
                normalizedPath = Path.Combine(normalizedPath, part);

            return normalizedPath;
        }

        protected void StartInterActiveConsole()
        {    
            try
            {
                Regex isYesExpression = new Regex(@"(yes)|(y)", RegexOptions.IgnoreCase);
                Console.WriteLine("No file or path to file have been specified as argument to the Program. Do yoy want to specifiy a file? \n");
                Console.WriteLine("Enter y or yes to continue. To quit or close the application, type q, quit or anything other than yes \n ");
                var answer = Console.ReadLine();

                if (!isYesExpression.IsMatch(answer))
                    Environment.Exit(0);

                if (isYesExpression.IsMatch(answer))
                {
                    Console.WriteLine("Enter directory or file path below \n");
                    var filePath = this.CleanUpPathCommandLineArguments(new string[] { Console.ReadLine() ?? "" });
                    if(filePath.Length > 0)
                        this.MessagePrinter.PrintFilesMatchingSearchTermsMessage(this.FileScanner.ScanFileForSearchTerm(filePath[0]));

                    return;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured when a file with the entered filePath scanned, Details: {0}", e.Message);
                // Exit method
                Environment.Exit(0);
            }
        }
    }
}
