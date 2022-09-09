using FileReader.Interfaces;
using FileReader.Utilities;
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
                return; // Exit method
            }

            if (cleanedUpCommandLineArguments.Length > 1)
                this.MessagePrinter.PrintFilesMatchingSearchTermsMessage(this.FileScanner.ScanFilesForSearchTerm(cleanedUpCommandLineArguments));
            else
                this.MessagePrinter.PrintFilesMatchingSearchTermsMessage(this.FileScanner.ScanFileForSearchTerm(cleanedUpCommandLineArguments[0]));
        }
        protected string StandardizeFilePath(string attemptedPath)
        {
            string normalizedPath = string.Empty;
            foreach (string part in attemptedPath.Split("-DIRECTORY-"))
                normalizedPath = Path.Combine(normalizedPath, part);

            return normalizedPath;
        }
    }
}
