using FileReader.Utilities;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FileReaderUnitTests
{
    public class FileScannerUnitTests
    {
        protected string TestFilePath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources");

        [Fact]
        public void SingleExistingFilePathSHouldGetMatches()
        {
            // Arrange
            var fileReaderService = new FileScanner(new FileReaderService());

            // Act
            var result = fileReaderService.ScanFileForSearchTerm(Path.Combine(this.TestFilePath, "TestFil.txt"));

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.True(result.First().NumberOfMatches == 11);
            Assert.True(result.First().SearchTerm == "TestFil");
        }

        [Fact]
        public void SingleExistingFileDirectoryShouldGetMatches()
        {
            // Arrange
            var fileReaderService = new FileScanner(new FileReaderService());

            // Act
            var result = fileReaderService.ScanFileForSearchTerm(this.TestFilePath);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            Assert.True(result[0].NumberOfMatches == 11);
            Assert.True(result[1].NumberOfMatches == 8);
            Assert.True(result[0].SearchTerm == "TestFil");
            Assert.True(result[1].SearchTerm == "TestFil2");
        }

        [Fact]
        public void SingleNonExistingFilePathShouldNotGetMatches()
        {
            // Arrange
            var fileReaderService = new FileScanner(new FileReaderService());

            // Act
            var result = fileReaderService.ScanFileForSearchTerm(Path.Combine(Path.Combine(this.TestFilePath, "NonExistingFolder"), "NonExistingFile.txt"));

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void ScanFilesForNonExistingFileDirectoryShouldNotGetMatches()
        {
            // Arrange
            var fileReaderService = new FileScanner(new FileReaderService());

            // Act
            var result = fileReaderService.ScanFileForSearchTerm(Path.Combine(this.TestFilePath, "NonExistingFolder"));

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }
    }
}
