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
            Name = Path.GetDirectoryName(directoryPath);
        }

        public IEnumerable<CustomDirectoryInfo> GetDirectories()
        {
            return new DirectoryInfo(FullName)
                .GetDirectories()
                .Select(x => new CustomDirectoryInfo(x.FullName));
        }

        public IEnumerable<CustomFileInfo> GetFiles()
        {
            return new DirectoryInfo(FullName)
                .GetFiles()
                .Select(x => new CustomFileInfo(x.FullName));
        }
    }
}
