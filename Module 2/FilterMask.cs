using System;

namespace FileSystemVisitorApp
{
    [Flags]
    public enum FilterMask
    {
        None = 0,
        IgnoreFilterFunction = 1 << 0,
        SortByName = 1 << 1,
        NoFolders = 1 << 2,
        FirstOnly = 1 << 3
    }
}
