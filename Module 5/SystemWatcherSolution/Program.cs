using System;
using System.Configuration;
using System.IO;
using System.Linq;
using SystemWatcherSolution.Models.Configuration;
using SystemWatcherSolution.Services;
using SystemWatcherSolution.Services.Converting;
using SystemWatcherSolution.Services.Validation;

namespace SystemWatcherSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var ruleValidationService = new RuleValidation();
            var watchedDirectoryValidationService = new WatchedDirectoryValidation();

            var ruleConvertionService = new RuleConverter(ruleValidationService);
            var watchedDirectoryConvertionService = new WatchedDirectoryConverter(watchedDirectoryValidationService);
            var systemWatcherConvertionService = new SystemWatcherConverter(ruleConvertionService, watchedDirectoryConvertionService);

            var systemWathcerSection = (SystemWatcherConfigurationSection)
                ConfigurationManager.GetSection("systemWatcher");

            var systemWatcher = systemWatcherConvertionService.Convert(systemWathcerSection);

            // Todo: implement fabric to configure multiple directories for CustomFileSystemWatcher
            var watcher = new CustomFileSystemWatcher();
            watcher.Path = systemWatcher.WatchedDirectories.FirstOrDefault().DirectoryInfo.FullName;

            var notifyFilters = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.NotifyFilter = notifyFilters;

            // Tracking all files due to "real" filtration via regular expressions from rules
            watcher.Filter = "*.*";

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            watcher.EnableRaisingEvents = true;

            //try
            //{
            //    Console.WriteLine("Press \'q\' to quit the sample.");
            //    while (Console.Read() != 'q') ;
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine($"Something went wrong {exception.Message}");
            //}
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