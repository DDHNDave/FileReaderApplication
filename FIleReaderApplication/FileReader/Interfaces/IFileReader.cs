using FileReader.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.Interfaces
{
    public interface IFileReader
    {
        List<FileScanResult> CountNumberOfTimesOwnFileNameInFile(string path);
    }
}
