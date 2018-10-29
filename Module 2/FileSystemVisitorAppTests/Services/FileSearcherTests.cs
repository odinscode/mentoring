using FileSystemVisitorApp.Services;
using Xunit;

namespace FileSystemVisitorAppTests.Services
{
    public class FileSearcherTests
    {
        private readonly string DefaultSearchPath = @"X:\Temp";

        [Fact]
        public void GetAllFilesRecursively_WhenMaskEmpty_ShouldReturnAllFilesAndDirectories()
        {
            // Arrange
            var fileSearcher = new FileSearcher(DefaultSearchPath);

            // Act

            // Assert

        }
    }
}
