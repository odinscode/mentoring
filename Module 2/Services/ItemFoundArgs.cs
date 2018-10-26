using System;
using System.IO;

namespace FileSystemVisitorApp.Services
{
    public class ItemFoundArgs : EventArgs
    {
        public FileSystemInfo Item { get; set; }
    }
}