using FileReader.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.Interfaces
{
    public interface IFileScanner
    {
        List<FileScanResult> ScanFileForSearchTerm(string path, string searchTerm = null);

        List<FileScanResult> ScanFilesForSearchTerm(string[] paths, string searchTerm = null);
    }
}
