using System.Collections.Generic;

namespace FileSystemVisitorApp.Services
{
    public interface IFileSearcher
    {
        IEnumerable<string> GetAllFilesRecursively(string directoryPath);
    }
}
