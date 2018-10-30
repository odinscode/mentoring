using System.Collections.Generic;

namespace FileSystemVisitorApp.Models
{
    public interface ICustomDirectoryInfo
    {
        /// <summary>
        /// Returns a file list from the current directory.
        /// </summary>
        /// <returns>An enumerable collection of FileItem in the current directory.</returns>
        IEnumerable<CustomFileInfo> GetFiles();

        /// <summary>
        /// Returns the subdirectories of the current directory.
        /// </summary>
        /// <returns>An enumerable collection of DirectoryItem in the current directory.</returns>
        IEnumerable<CustomDirectoryInfo> GetDirectories();
    }
}
