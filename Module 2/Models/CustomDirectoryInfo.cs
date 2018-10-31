using FileSystemVisitorApp.Models.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemVisitorApp.Models
{
    public class CustomDirectoryInfo : CustomFileItem, ICustomDirectoryInfo
    {
        public CustomDirectoryInfo(string directoryPath)
        {
            FullName = directoryPath;
            Name = Path.GetFileName(directoryPath);
        }

        public CustomDirectoryInfo CreateInstance(string directoryPath)
        {
            return new CustomDirectoryInfo(directoryPath);
        }

        public virtual IEnumerable<CustomDirectoryInfo> GetDirectories()
        {
            return new DirectoryInfo(FullName)
                .GetDirectories()
                .Select(x => new CustomDirectoryInfo(x.FullName));
        }

        public virtual IEnumerable<CustomFileInfo> GetFiles()
        {
            return new DirectoryInfo(FullName)
                .GetFiles()
                .Select(x => new CustomFileInfo(x.FullName));
        }
    }
}
