using FileSystemVisitorApp.Models;

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
