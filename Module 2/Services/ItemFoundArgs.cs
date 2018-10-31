using FileSystemVisitorApp.Models;
using System;

namespace FileSystemVisitorApp.Services
{
    public class ItemFoundArgs : EventArgs
    {
        public CustomFileItem Item { get; set; }
    }
}