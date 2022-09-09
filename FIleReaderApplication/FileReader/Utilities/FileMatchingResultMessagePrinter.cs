using FileReader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileReader.Utilities
{
    public class FileMatchingResultMessagePrinter
    {
        protected void PrintNoFilesMessage()
        {
            Console.WriteLine("No Files where scanned");
        }

        public void PrintFilesMatchingSearchTermsMessage(List<FileScanResult> searchFileForFileNameResults)
        {
            if (searchFileForFileNameResults == null || !(searchFileForFileNameResults?.Any() ?? true))
            {
                this.PrintNoFilesMessage();
                // exit method
                return;
            }
                

            Console.WriteLine("Number of files searched: {0} \n", searchFileForFileNameResults.Count);
            Console.WriteLine("Files with with content matching search term, Details: \n");

            foreach (FileScanResult scanResult in searchFileForFileNameResults)
            {
                if (scanResult.NumberOfMatches > 1)
                    Console.WriteLine(
                        "File name: {0} Search Term: {1} Number of matches {2} \n",
                        scanResult.FileName,
                        scanResult.SearchTerm,
                        scanResult.NumberOfMatches
                    );
            }
        }
    }
}
