using FileSystemVisitorApp.Services;
using System;
using System.IO;

namespace FileSystemVisitorApp
{
    class Program
    {
        private const string DefaultSearchPath = @"C:\Projects";

        static bool FilterFunction(FileSystemInfo file)
        {
            return file.FullName.ToLower().EndsWith(".cs");
        }

        static void Main(string[] args)
        {
            var fileSearcher = new FileSearcher(DefaultSearchPath, FilterFunction);
            GetSubscribers(fileSearcher);

            var options = GetMaskConfiguration();
            var result = fileSearcher.GetAllFilesRecursively(options);
        }

        private static FilterMask GetMaskConfiguration()
        {
            return FilterMask.None;
        }

        private static void GetSubscribers(FileSearcher fileSearcher)
        {
            fileSearcher.SearchStarted += Visitor_Start;
            fileSearcher.SearchFinished += Visitor_Finish;

            fileSearcher.FileFound += Visitor_FileFound;
            fileSearcher.DirectoryFound += Visitor_DirectoryFound;

            fileSearcher.FilteredFileFound += Visitor_FilteredFileFound;
            fileSearcher.FilteredDirectoryFound += Visitor_FilteredDirectoryFound;
        }

        private static void Visitor_FilteredDirectoryFound(FileSearcher sender, ItemFoundArgs e)
        {
            Console.WriteLine($"Filtered directory found: {e.Item.FullName}");
        }

        private static void Visitor_FilteredFileFound(FileSearcher sender, ItemFoundArgs e)
        {
            Console.WriteLine($"Filtered file found: {e.Item.FullName}");
        }

        private static void Visitor_DirectoryFound(FileSearcher sender, ItemFoundArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Directory found: {e.Item.FullName}");
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private static void Visitor_FileFound(FileSearcher sender, ItemFoundArgs e)
        {
            Console.WriteLine($"File found: {e.Item.FullName}");
        }

        private static void Visitor_Start(object sender, EventArgs e)
        {
            Console.WriteLine("Search started");
        }

        private static void Visitor_Finish(object sender, EventArgs e)
        {
            Console.WriteLine("Search finished");
        }
    }
}
