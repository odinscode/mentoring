using System.Collections.Generic;
using System.IO;

namespace FileSystemVisitorApp.Services
{
    public class FileSearcher : IFileSearcher
    {
        public string DirectoryPath { get; set; }

        public FileSearcher(string directoryPath)
        {
            this.DirectoryPath = directoryPath;
        }

        public IEnumerable<string> GetAllFilesRecursively(string directoryPath)
        {
            var directoryInfo = new DirectoryInfo(this.DirectoryPath);
            var directories = directoryInfo.GetDirectories();
            var files = directoryInfo.GetFiles();
        }
    }
}
