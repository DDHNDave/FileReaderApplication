using FileReader.Interfaces;
using FileReader.Model;
using System;
using System.Collections.Generic;

namespace FileReader.Utilities
{
    public class FileScanner : IFileScanner
    {
        public FileScanner(IFileReader fileReader)
        {
            this.FileReader = fileReader;
        }

        protected void HandleEmptyResultList(List<FileScanResult> results, string[] paths)
        {
            if (results == null || (results?.Count ?? 1)  == 0)
            {
                foreach (string path in paths)
                    Console.WriteLine("No file or files could be found for path {0} \n", path);
            }
        }

        protected readonly IFileReader FileReader;

        public List<FileScanResult> ScanFileForSearchTerm(string path, string searchTerm = null)
        {
            try
            {
                var fileScanResult = this.FileReader.CountNumberOfTimesOwnFileNameInFile(path);
                this.HandleEmptyResultList((fileScanResult ?? new List<FileScanResult>()), new string[] { path });

                return fileScanResult;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured when attempting to scan file content");
                Console.WriteLine("Error details: {0} ", e.Message);

                return new List<FileScanResult>();
            }
            
        }

        public List<FileScanResult> ScanFilesForSearchTerm(string[] paths, string searchTerm = null)
        {
            try
            {
                var searchFileForFileNameResults = new List<FileScanResult>();
                foreach (string argument in paths)
                    searchFileForFileNameResults.AddRange(this.FileReader.CountNumberOfTimesOwnFileNameInFile(argument));

                this.HandleEmptyResultList(searchFileForFileNameResults, paths);
                return searchFileForFileNameResults;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured when attempting to scan file content");
                Console.WriteLine("Error details: {0} ", e.Message);

                return new List<FileScanResult>();
            }

        }


    }
}
