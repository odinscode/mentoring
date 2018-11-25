using System.Collections.Generic;
using SystemWatcherSolution.Models.Configuration;
using SystemWatcherSolution.Models.Entities;

namespace SystemWatcherSolution.Services.Converting
{
    public class SystemWatcherConverter 
        : IElementConverter<SystemWatcherConfigurationSection, SystemWatcher>
    {
        private readonly IElementConverter<RuleElement, Rule> ruleConvertionService;
        private readonly IElementConverter<WatchedDirectoryElement, WatchedDirectory> directoryConvertionService;

        public SystemWatcherConverter(
            IElementConverter<RuleElement, Rule> ruleConvertionService,
            IElementConverter<WatchedDirectoryElement, WatchedDirectory> directoryConvertionService
            )
        {
            this.ruleConvertionService = ruleConvertionService;
            this.directoryConvertionService = directoryConvertionService;
        }

        public SystemWatcher Convert(SystemWatcherConfigurationSection source)
        {
            SystemWatcher systemWatcher = new SystemWatcher();

            systemWatcher.Culture = source.CultureInfo.CurrentCulture;

            var rules = new List<Rule>();
            foreach (RuleElement ruleElement in source.Rules)
            {
                var rule = this.ruleConvertionService.Convert(ruleElement);
                rules.Add(rule);
            }
            systemWatcher.Rules = rules;

            var directories = new List<WatchedDirectory>();
            foreach (WatchedDirectoryElement watchedDirectoryElement in source.WatchedDirectories)
            {
                var directory = this.directoryConvertionService.Convert(watchedDirectoryElement);
                directories.Add(directory);
            }
            systemWatcher.WatchedDirectories = directories;

            return systemWatcher;
        }
    }
}
