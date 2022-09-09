﻿using FileReader.Interfaces;
using FileReader.Utilities;
using System;
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

        protected string processPathCommandLineArguments(string[] arguments)
        {
            Regex normalizePathDividerExpression = new Regex(@"[\\\/]", RegexOptions.IgnoreCase);

            foreach(string path in arguments)

        }

        public void Run(string[] arguments)
        {
            if (arguments == null || (arguments?.Length ?? 0) == 0)
            {
                Console.WriteLine("No filepath or filePaths have been specified");
                return; // Exit method
            }

            if (arguments.Length > 1)
                this.MessagePrinter.PrintFilesMatchingSearchTermsMessage(this.FileScanner.ScanFilesForSearchTerm(arguments));
            else
                this.MessagePrinter.PrintFilesMatchingSearchTermsMessage(this.FileScanner.ScanFileForSearchTerm(arguments[0]));
        }
    }
}
