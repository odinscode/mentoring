using FileSystemVisitorApp.Models;
using FileSystemVisitorApp.Models.Interfaces;
using FileSystemVisitorApp.Services.Interfaces;

namespace FileSystemVisitorApp.Services
{
    class CustomDirectoryInfoFactory : ICustomDirectoryInfoFactory
    {
        public ICustomDirectoryInfo CreateInstance(string directoryPath)
        {
            return new CustomDirectoryInfo(directoryPath);
        }
    }
}
