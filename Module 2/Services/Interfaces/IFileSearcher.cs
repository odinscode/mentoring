using FileSystemVisitorApp.Models;

namespace FileSystemVisitorApp.Services.Interfaces
{
    public interface IFileSearcher
    {
        FileSystemInfoCustomCollection<CustomFileItem> GetItemsRecursively(FilterMask filterMask);
    }
}
