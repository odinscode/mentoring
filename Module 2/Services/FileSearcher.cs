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

        // Todo: rework method
        public FileSystemInfoCustomCollection<FileSystemInfo> GetAllFilesRecursively(string directoryPath)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);
            var directoryCollection = new FileSystemInfoCustomCollection<FileSystemInfo>();
            directoryCollection.Add(directoryInfo);
            var files = directoryInfo.GetFiles();

            foreach (var file in files)
            {
                directoryCollection.Add(file);
            }

            var directories = directoryInfo.GetDirectories();

            foreach (var directory in directories)
            {
                var result = GetAllFilesRecursively(directory.FullName);
                foreach (var item in result)
                {
                    directoryCollection.Add(item);
                }
            }

            return directoryCollection;
        }
    }
}
