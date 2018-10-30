using FileSystemVisitorApp.Models;

namespace FileSystemVisitorApp.Services
{
    public interface ICustomDirectoryInfoFactory
    {
        ICustomDirectoryInfo CreateInstance(string directoryPath);
    }
}
