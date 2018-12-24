using System;
using System.Configuration;
using System.Globalization;
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
        private static bool _isCanceled;

        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            InitializeSystemWatchers();

            try
            {
                Console.WriteLine("Press Ctrl+C or Ctrl+Break to quit the sample.");
                while (!_isCanceled);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Something went wrong {exception.Message}");
            }
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if (e.SpecialKey == ConsoleSpecialKey.ControlC 
                || e.SpecialKey == ConsoleSpecialKey.ControlBreak)
            {
                _isCanceled = true;
                e.Cancel = true;
            }
        }

        private static void InitializeSystemWatchers()
        {
            var systemWatcherSettings = GetSystemWatcherSettings();

            // todo: think about better place to setup culture
            SetupCultureFromConfigFile(systemWatcherSettings.Culture);

            var customSystemWatcherFactory = new CustomFileSystemWatcherFactory();
            foreach (var watchedDirectory in systemWatcherSettings.WatchedDirectories)
            {
                customSystemWatcherFactory.CreateInstance(watchedDirectory.DirectoryInfo.FullName, systemWatcherSettings.Rules.ToList(), systemWatcherSettings.DefaultDirectory);
            }
        }

        private static void SetupCultureFromConfigFile(CultureInfo culture)
        {
            CultureInfo.CurrentCulture = culture;
            // todo: is it necessary to specify UI culture?
            //CultureInfo.CurrentUICulture = culture;
        }

        private static SystemWatcher GetSystemWatcherSettings()
        {
            var ruleValidationService = new RuleValidation();
            var watchedDirectoryValidationService = new WatchedDirectoryValidation();

            var ruleConvertionService = new RuleConverter(ruleValidationService);
            var watchedDirectoryConvertionService = new WatchedDirectoryConverter(watchedDirectoryValidationService);
            var systemWatcherConvertionService = new SystemWatcherConverter(ruleConvertionService, watchedDirectoryConvertionService);

            var systemWathcerSection = (SystemWatcherConfigurationSection)
                ConfigurationManager.GetSection("systemWatcher");

            return systemWatcherConvertionService.Convert(systemWathcerSection);
        }
    }
}