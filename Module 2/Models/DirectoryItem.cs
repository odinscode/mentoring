using System.Collections.Generic;

namespace FileSystemVisitorApp.Models
{
    public class DirectoryItem : Item
    {
        public IEnumerable<Item> Children { get; set; }
    }
}
