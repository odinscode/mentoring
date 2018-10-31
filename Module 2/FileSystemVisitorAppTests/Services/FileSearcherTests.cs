using FileSystemVisitorApp;
using FileSystemVisitorApp.Models;
using FileSystemVisitorApp.Services;
using Moq;
using System;
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
            var filesRootMock = GetMockedFiles(new string[] { FirstIterationFileName });
            var filesSecondIterationMock = GetMockedFiles(new string[] { SecondIterationFileName });

            var directoriesFirstIterationMock = new Mock<List<Mock<CustomDirectoryInfo>>>();
            var directoriesSecondIterationMock = GetMockedDirectory(SecondIterationDirectoryPath, filesSecondIterationMock);
            directoriesFirstIterationMock.Object.Add(directoriesSecondIterationMock);

            var directoryRootMock = GetMockedDirectory(RootDirectoryPath, filesRootMock, directoriesFirstIterationMock);

            var factoryInstances = new Dictionary<string, Mock<CustomDirectoryInfo>>();
            factoryInstances.Add(RootDirectoryPath, directoryRootMock);
            factoryInstances.Add(SecondIterationDirectoryPath, directoriesSecondIterationMock);

            var factoryMock = GetMockedFactories(factoryInstances);

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

        private Mock<ICustomDirectoryInfoFactory> GetMockedFactories(Dictionary<string, Mock<CustomDirectoryInfo>> instances)
        {
            var factoryMock = new Mock<ICustomDirectoryInfoFactory>();
            foreach (var instanceName in instances.Keys)
            {
                factoryMock.Setup(_ => _.CreateInstance(instanceName))
                    .Returns(instances[instanceName].Object);
            }
            return factoryMock;
        }

        private Mock<List<Mock<CustomFileInfo>>> GetMockedFiles(string[] fileNames)
        {
            var filesRootMock = new Mock<List<Mock<CustomFileInfo>>>();
            foreach (var fileName in fileNames)
            {
                var fileMock = new Mock<CustomFileInfo>(fileName);
                filesRootMock.Object.Add(fileMock);
            }
            return filesRootMock;
        }

        private Mock<CustomDirectoryInfo> GetMockedDirectory(string directoryPath)
        {
            var directoryMock = new Mock<CustomDirectoryInfo>(directoryPath);
            return directoryMock;
        }

        private Mock<CustomDirectoryInfo> GetMockedDirectory(string directoryPath, Mock<List<Mock<CustomFileInfo>>> internalFiles = null)
        {
            var directoryMock = GetMockedDirectory(directoryPath);
            directoryMock.Setup(_ => _.GetFiles())
                .Returns(internalFiles.Object.Select(x => x.Object));
            return directoryMock;
        }

        private Mock<CustomDirectoryInfo> GetMockedDirectory(string directoryPath, Mock<List<Mock<CustomFileInfo>>> internalFiles, Mock<List<Mock<CustomDirectoryInfo>>> internalDirectories)
        {
            var directoryMock = GetMockedDirectory(directoryPath, internalFiles);
            directoryMock.Setup(_ => _.GetDirectories())
                .Returns(internalDirectories.Object.Select(x => x.Object));
            return directoryMock;
        }
    }
}
