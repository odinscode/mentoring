using FileSystemVisitorApp.Models;
using System;

namespace FileSystemVisitorApp.Services
{
    public class FileSearcher : IFileSearcher
    {
        #region Events

        public event EventHandler SearchStarted;
        public event EventHandler SearchFinished;

        public event ItemFoundHandler FileFound;
        public event ItemFoundHandler DirectoryFound;

        public event ItemFoundHandler FilteredFileFound;
        public event ItemFoundHandler FilteredDirectoryFound;

        #endregion

        private readonly ICustomDirectoryInfoFactory customDirectoryInfoFactory;

        private Func<CustomFileItem, bool> FileFilterDelegate;
        public delegate void ItemFoundHandler(FileSearcher sender, ItemFoundArgs e);

        public string DirectoryPath { get; set; }

        public FileSearcher(ICustomDirectoryInfoFactory customDirectoryInfoFactory, string directoryPath) 
            : this(customDirectoryInfoFactory, directoryPath, (file) => { return true; })
        {

        }

        public FileSearcher(ICustomDirectoryInfoFactory customDirectoryInfoFactory, string directoryPath, Func<CustomFileItem, bool> func)
        {
            this.customDirectoryInfoFactory = customDirectoryInfoFactory;
            DirectoryPath = directoryPath;
            FileFilterDelegate = func;
        }

        private bool isFirstFilteredFileFound = false;

        private FileSystemInfoCustomCollection<CustomFileItem> GetItemsRecursively(string directoryPath, FilterMask filterMask)
        {
            var directoryInfo = (CustomDirectoryInfo)customDirectoryInfoFactory.CreateInstance(directoryPath);

            var output = new FileSystemInfoCustomCollection<CustomFileItem>();

            if (isFirstFilteredFileFound && filterMask.HasFlag(FilterMask.FirstOnly))
                return output;

            if (!filterMask.HasFlag(FilterMask.NoFolders))
            {
                DirectoryFound?.Invoke(this, new ItemFoundArgs { Item = directoryInfo });
                if (filterMask.HasFlag(FilterMask.IgnoreFilterFunction) || FileFilterDelegate.Invoke(directoryInfo))
                {
                    FilteredDirectoryFound?.Invoke(this, new ItemFoundArgs { Item = directoryInfo });
                    output.Add(directoryInfo);
                    isFirstFilteredFileFound = true;
                    if (isFirstFilteredFileFound && filterMask.HasFlag(FilterMask.FirstOnly))
                        return output;
                }
            }

            var files = directoryInfo.GetFiles();

            foreach (var file in files)
            {
                FileFound?.Invoke(this, new ItemFoundArgs { Item = file });
                if (filterMask.HasFlag(FilterMask.IgnoreFilterFunction) || FileFilterDelegate.Invoke(file))
                {
                    FilteredFileFound?.Invoke(this, new ItemFoundArgs { Item = file });
                    output.Add(file);
                    isFirstFilteredFileFound = true;
                    if (isFirstFilteredFileFound && filterMask.HasFlag(FilterMask.FirstOnly))
                        return output;
                }
            }

            var directories = directoryInfo.GetDirectories();

            foreach (var directory in directories)
            {
                var result = GetItemsRecursively(directory.FullName, filterMask);
                foreach (var item in result)
                {
                    output.Add(item);
                }
            }

            return output;
        }

        public FileSystemInfoCustomCollection<CustomFileItem> GetItemsRecursively(FilterMask filterMask)
        {
            isFirstFilteredFileFound = false;

            SearchStarted?.Invoke(this, new EventArgs());
            var output = GetItemsRecursively(DirectoryPath, filterMask);
            SearchFinished?.Invoke(this, new EventArgs());

            if (filterMask.HasFlag(FilterMask.SortByName))
                output.Sort();

            return output;
        }
    }
}
