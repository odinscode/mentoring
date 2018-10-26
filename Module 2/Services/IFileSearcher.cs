using System.IO;

namespace FileSystemVisitorApp.Services
{
    public interface IFileSearcher
    {
        FileSystemInfoCustomCollection<FileSystemInfo> GetAllFilesRecursively(FilterMask filterMask);
    }
}
