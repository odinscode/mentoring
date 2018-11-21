using System;
using System.Configuration;
using System.IO;
using SystemWatcherSolution.Infrastructure;
using SystemWatcherSolution.Models.Configuration;
using SystemWatcherSolution.Models.Entities;
using SystemWatcherSolution.Services;
using Unity;

namespace SystemWatcherSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterDependencies.SetupContainer();
            var container = RegisterDependencies.Container;

            try
            {
                var systemWatcherSection = (SystemWatcherConfigurationSection)
                    ConfigurationManager.GetSection("systemWatcher");

                var watchedDirectories = systemWatcherSection.WatchedDirectories;

                foreach (WatchedDirectoryElement watchedDirectory in watchedDirectories)
                {
                    Console.WriteLine(watchedDirectory.DirectoryPath);
                }

                var watcher = new CustomFileSystemWatcher();
                watcher.Path = systemWatcherSection.Rules.DefaultDirectoryPath;

                SetupWatcherNotifyFilters(watcher);
                SetupWatcherFilter(watcher);
                SetupWatcherEventHandlers(watcher);

                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Press \'q\' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Something went wrong {exception.Message}");
            }
        }

        private static void SetupWatcherPath(CustomFileSystemWatcher watcher, string directoryPath)
        {
            watcher.Path = directoryPath;
        }

        private static void SetupWatcherNotifyFilters(CustomFileSystemWatcher watcher)
        {
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                | NotifyFilters.FileName | NotifyFilters.DirectoryName;
        }

        private static void SetupWatcherFilter(CustomFileSystemWatcher watcher)
        {
            watcher.Filter = "*.txt";
        }

        private static void SetupWatcherEventHandlers(CustomFileSystemWatcher watcher)
        {
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }
    }
}