using FileReader.Interfaces;
using FileReader.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileReader.Utilities
{
    public class FileReaderService : IFileReader
    {
        protected void AddFilesFromDirectory(string targetDirectory, List<string> foundFiles, int recursionLevel = 0)
        {
            if (recursionLevel > 5 || foundFiles.Count > 100)
                return;

            if (foundFiles == null)
                foundFiles = new List<string>();

            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foundFiles.AddRange(fileEntries);

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            if (!subdirectoryEntries.Any())
                return;

            foreach (string subdirectory in subdirectoryEntries)
                this.AddFilesFromDirectory(subdirectory, foundFiles, recursionLevel++);
        }

        public List<FileScanResult> CountNumberOfTimesOwnFileNameInFile(string path)
        {
            if (File.Exists(path))
                return new List<FileScanResult>() { this.SearchFileForContent(this.FindFileInfo(path), this.FindFileInfo(path).Name) };

            if (Directory.Exists(path))
            {
                List<string> fileNames = new List<string>();
                List<FileScanResult> matches = new List<FileScanResult>();
                this.AddFilesFromDirectory(path, fileNames);

                foreach (string file in fileNames)
                {
                    var subDirectoryFileScanResult = this.SearchFileForContent(this.FindFileInfo(file), this.FindFileInfo(file).Name);
                    if (subDirectoryFileScanResult != null)
                        matches.Add(subDirectoryFileScanResult);
                }

                return matches;
            }

            return new List<FileScanResult>();
        }

        protected FileInfo FindFileInfo(string filename) => new FileInfo(filename);


        protected FileScanResult SearchFileForContent(FileInfo file, string searchItem)
        {
            var result = default(FileScanResult);
            if (!file.Exists || string.IsNullOrEmpty(Regex.Replace(searchItem, $@"{file.Extension}", string.Empty)))
                return result;

            result = new FileScanResult()
            {
                FileName = file.Name,
                NumberOfMatches = 0,
                SearchTerm = Regex.Replace(searchItem, $@"{file.Extension}", string.Empty)
            };

            Regex expression = new Regex($@"({result.SearchTerm})", RegexOptions.IgnoreCase);

            using (StreamReader reader = file.OpenText())
            {
                bool endOfFile;
                string fileLine = !reader.EndOfStream ? null : reader.ReadLine();

                do
                {
                    var matches = expression.Matches(fileLine ?? string.Empty);
                    if (matches.Any())
                        result.NumberOfMatches += matches.Count;

                    fileLine = reader.ReadLine();
                    endOfFile = reader.EndOfStream;

                    // The last line will not be scanned if the end of file is reached
                    if (endOfFile && fileLine != null)
                        result.NumberOfMatches += expression.Matches(fileLine).Count;

                } while (!endOfFile);
            }

            return result;
        }
    }
}
