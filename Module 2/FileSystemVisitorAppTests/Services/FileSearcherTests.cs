using FileSystemVisitorApp;
using FileSystemVisitorApp.Models;
using FileSystemVisitorApp.Services;
using FileSystemVisitorApp.Services.Interfaces;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace FileSystemVisitorAppTests.Services
{
    public class FileSearcherTests
    {
        private static readonly string RootPath = @"X:\Temp";
        private static readonly string SecondIterationPath = $@"{RootPath}\Directory1";
        private static readonly string FirstIterationFileName = $@"{RootPath}\Test1.txt";
        private static readonly string SecondIterationFileName = $@"{SecondIterationPath}\Test2.txt";

        [Fact]
        public void GetItemsRecursively_WhenMaskNone_ShouldReturnAllFilesAndDirectories()
        {
            // Arrange
            var fileSearcher = SetupFileSearcher();
            var filterMask = FilterMask.None;

            // Act
            var result = fileSearcher.GetItemsRecursively(filterMask);

            // Assert
            var filesCount = result.Where(x => x is CustomFileInfo).Count();
            var directoriesCount = result.Where(x => x is CustomDirectoryInfo).Count();

            Assert.Equal(2, filesCount);
            Assert.Equal(2, directoriesCount);
        }

        [Fact]
        public void GetItemsRecursively_WhenMaskNoFolders_ShouldReturnOnlyFiles()
        {
            // Arrange
            var fileSearcher = SetupFileSearcher();
            var filterMask = FilterMask.NoFolders;

            // Act
            var result = fileSearcher.GetItemsRecursively(filterMask);

            // Assert
            var filesCount = result.Where(x => x is CustomFileInfo).Count();
            var directoriesCount = result.Where(x => x is CustomDirectoryInfo).Count();

            Assert.Equal(2, filesCount);
            Assert.Equal(0, directoriesCount);
        }

        [Fact]
        public void GetItemsRecursively_WhenMaskFirstOnly_ShouldReturnFirstItem()
        {
            // Arrange
            var fileSearcher = SetupFileSearcher();
            var filterMask = FilterMask.FirstOnly;

            // Act
            var result = fileSearcher.GetItemsRecursively(filterMask);

            // Assert
            var filesCount = result.Where(x => x is CustomFileInfo).Count();
            var directoriesCount = result.Where(x => x is CustomDirectoryInfo).Count();

            Assert.Equal(0, filesCount);
            Assert.Equal(1, directoriesCount);
            Assert.Equal(Path.GetFileName(RootPath), result.FirstOrDefault().Name);
        }

        [Fact]
        public void GetItemsRecursively_WhenMaskSortByName_ShouldReturnSortedItems()
        {
            // Arrange
            var customDirectory = new string[]
            {
                SecondIterationPath,            // X:\Temp\Directory1
                RootPath,                       // X:\Temp
                FirstIterationFileName,         // X:\Temp\Test1.txt
                SecondIterationFileName         // X:\Temp\Directory1\Test2.txt
            };

            var expectedResult = customDirectory.Select(x => Path.GetFileName(x)).ToArray();

            var fileSearcher = SetupFileSearcher();
            var filterMask = FilterMask.SortByName;

            // Act
            var result = fileSearcher.GetItemsRecursively(filterMask);

            // Assert
            var filesCount = result.Where(x => x is CustomFileInfo).Count();
            var directoriesCount = result.Where(x => x is CustomDirectoryInfo).Count();

            var itemsSortedNames = result.Select(i => i.Name).ToArray();

            for (int i = 0; i < itemsSortedNames.Count(); i++)
            {
                Assert.Equal(expectedResult[i], itemsSortedNames[i]);
            }

            Assert.Equal(2, filesCount);
            Assert.Equal(2, directoriesCount);
        }

        private FileSearcher SetupFileSearcher()
        {
            var filesRootMock = GetMockedFiles(new string[] { FirstIterationFileName });
            var filesSecondIterationMock = GetMockedFiles(new string[] { SecondIterationFileName });

            var directoriesFirstIterationMock = new Mock<List<Mock<CustomDirectoryInfo>>>();
            var directoriesSecondIterationMock = GetMockedDirectory(SecondIterationPath, filesSecondIterationMock);
            directoriesFirstIterationMock.Object.Add(directoriesSecondIterationMock);

            var directoryRootMock = GetMockedDirectory(RootPath, filesRootMock, directoriesFirstIterationMock);

            var factoryInstances = new Dictionary<string, Mock<CustomDirectoryInfo>>();
            factoryInstances.Add(RootPath, directoryRootMock);
            factoryInstances.Add(SecondIterationPath, directoriesSecondIterationMock);

            var factoryMock = GetMockedFactories(factoryInstances);

            var fileSearcher = new FileSearcher(factoryMock.Object, RootPath);

            return fileSearcher;
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
