using FileSystemVisitorApp.Models;

namespace FileSystemVisitorApp.Services
{
    public interface IFileSearcher
    {
        FileSystemInfoCustomCollection<CustomFileItem> GetItemsRecursively(FilterMask filterMask);
    }
}
