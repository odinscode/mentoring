using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using SystemWatcherSolution.Models.Configuration;
using SystemWatcherSolution.Models.Entities;
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
            var path = systemWatcher.WatchedDirectories.FirstOrDefault().DirectoryInfo.FullName;
            var watcher = new CustomFileSystemWatcher(path, new List<Rule>(systemWatcher.Rules));

            try
            {
                Console.WriteLine("Press \'q\' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Something went wrong {exception.Message}");
            }
        }
    }
}