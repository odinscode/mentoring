using FileSystemVisitorApp.Models;
using FileSystemVisitorApp.Models.Interfaces;

namespace FileSystemVisitorApp.Services.Interfaces
{
    public interface ICustomDirectoryInfoFactory
    {
        ICustomDirectoryInfo CreateInstance(string directoryPath);
    }
}
