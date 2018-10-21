using FileSystemVisitorApp.Services;

namespace FileSystemVisitorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSearcher = new FileSearcher(@"C:\Projects");
            var files = fileSearcher.GetAllFilesRecursively(fileSearcher.DirectoryPath);
            foreach (var file in files)
            {
                System.Console.WriteLine(file.FullName);
            }
        }
    }
}
