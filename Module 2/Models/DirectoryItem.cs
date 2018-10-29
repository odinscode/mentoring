using System.Collections.Generic;

namespace FileSystemVisitorApp.Models
{
    public class DirectoryItem : CustomFileItem
    {
        public IEnumerable<DirectoryItem> Subdirectories { get; private set; }

        public IEnumerable<CustomFileInfo> Files { get; private set; }

        public DirectoryItem(string directoryPath)
        {
            FullName = directoryPath;
            Name = System.IO.Path.GetDirectoryName(directoryPath);
            Subdirectories = new List<DirectoryItem>();
            Files = new List<CustomFileInfo>();
        }
    }
}
