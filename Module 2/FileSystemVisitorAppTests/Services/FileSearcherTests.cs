using FileSystemVisitorApp;
using FileSystemVisitorApp.Models;
using FileSystemVisitorApp.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FileSystemVisitorAppTests.Services
{
    public class FileSearcherTests
    {
        private static readonly string RootDirectoryPath = 
            @"X:\Temp";
        private static readonly string SecondIterationDirectoryPath = 
            string.Concat(RootDirectoryPath, @"\", "Directory1");

        private static readonly string FirstIterationFileName = 
            string.Concat(RootDirectoryPath, @"\", "Test1.txt");
        private static readonly string SecondIterationFileName = 
            string.Concat(SecondIterationDirectoryPath, @"\", "Test2.txt");

        [Fact]
        public void GetItemsRecursively_WhenMaskNone_ShouldReturnAllFilesAndDirectories()
        {
            // Arrange
            var directoriesFirstIterationMock = new Mock<List<Mock<CustomDirectoryInfo>>>();
            var directoriesSecondIterationMock = new Mock<CustomDirectoryInfo>(SecondIterationDirectoryPath);

            var filesSecondIterationMock = new Mock<List<Mock<CustomFileInfo>>>(); 
            var fileSecondIterationMock = new Mock<CustomFileInfo>(SecondIterationFileName);
            filesSecondIterationMock.Object.Add(fileSecondIterationMock);

            directoriesSecondIterationMock.Setup(_ => _.GetFiles())
                .Returns(filesSecondIterationMock.Object.Select(x => x.Object));
            directoriesFirstIterationMock.Object.Add(directoriesSecondIterationMock);

            var filesRootMock = new Mock<List<Mock<CustomFileInfo>>>();
            var fileFirstIterationMock = new Mock<CustomFileInfo>(FirstIterationFileName);
            filesRootMock.Object.Add(fileFirstIterationMock);

            var directoryRootMock = new Mock<CustomDirectoryInfo>(RootDirectoryPath);
            directoryRootMock.Setup(_ => _.GetDirectories())
                .Returns(directoriesFirstIterationMock.Object.Select(x => x.Object));
            directoryRootMock.Setup(_ => _.GetFiles())
                .Returns(filesRootMock.Object.Select(x => x.Object));

            var factoryMock = new Mock<ICustomDirectoryInfoFactory>();
            factoryMock.Setup(_ => _.CreateInstance(RootDirectoryPath))
                .Returns(directoryRootMock.Object);
            factoryMock.Setup(_ => _.CreateInstance(SecondIterationDirectoryPath))
                .Returns(directoriesSecondIterationMock.Object);

            var fileSearcher = new FileSearcher(factoryMock.Object, RootDirectoryPath);
            var filterMask = FilterMask.None;

            // Act
            var result = fileSearcher.GetItemsRecursively(filterMask);

            // Assert
            // TODO: do not use reflection
            var filesCount = result.Where(x => x.GetType().Name == "CustomFileInfoProxy").Count();
            var directoriesCount = result.Where(x => x.GetType().Name == "CustomDirectoryInfoProxy").Count();

            Assert.Equal(2, filesCount);
            Assert.Equal(2, directoriesCount);
        }
    }
}
