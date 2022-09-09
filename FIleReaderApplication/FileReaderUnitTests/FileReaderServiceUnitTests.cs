using FileReader.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FileReaderUnitTests
{
    public class FileReaderServiceUnitTests
    {
        protected string TestFilePath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources");

        [Fact]
        public void PathToExitingFileShouldFindMatches()
        {
            // Arrange
            var fileReaderService = new FileReaderService();

            // Act
            var result = fileReaderService.CountNumberOfTimesOwnFileNameInFile(Path.Combine(this.TestFilePath, "TestFil.txt"));

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.True(result.First().NumberOfMatches == 11);
            Assert.True(result.First().SearchTerm == "TestFil");
        }

        [Fact]
        public void PathToNonExitingFileShouldNotFindMatches()
        {
            // Arrange
            var fileReaderService = new FileReaderService();

            // Act
            var result = fileReaderService.CountNumberOfTimesOwnFileNameInFile(
                Path.Combine(
                    Path.Combine(this.TestFilePath, "NonExistingFolder"),
                    "NonexistingFile.txt"
                )
             );

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void PathToDirectoryShouldFindAllFilesAndFIndMatchers()
        {
            // Arrange
            var fileReaderService = new FileReaderService();

            // Act
            var result = fileReaderService.CountNumberOfTimesOwnFileNameInFile(this.TestFilePath);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            Assert.True(result[0].NumberOfMatches == 11);
            Assert.True(result[1].NumberOfMatches == 8);
            Assert.True(result[0].SearchTerm == "TestFil");
            Assert.True(result[1].SearchTerm == "TestFil2");
        }
    }
}
