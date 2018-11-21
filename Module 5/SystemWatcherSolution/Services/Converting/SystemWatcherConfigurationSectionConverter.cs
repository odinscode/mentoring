using System.Collections.Generic;
using SystemWatcherSolution.Models.Configuration;
using SystemWatcherSolution.Models.Entities;

namespace SystemWatcherSolution.Services.Converting
{
    public class SystemWatcherConfigurationSectionConverter 
        : IElementConverter<SystemWatcherConfigurationSection, SystemWatcher>
    {
        private readonly IElementConverter<RuleElementCollection, IEnumerable<Rule>> rulesConvertionService;
        private readonly IElementConverter<WatchedDirectoryElementCollection, IEnumerable<WatchedDirectory>> directoriesConvertionService;

        public SystemWatcherConfigurationSectionConverter(
            IElementConverter<RuleElementCollection, IEnumerable<Rule>> rulesConvertionService,
            IElementConverter<WatchedDirectoryElementCollection, IEnumerable<WatchedDirectory>> directoriesConvertionService
            )
        {
            this.rulesConvertionService = rulesConvertionService ??
                throw new System.ArgumentNullException("RuleElementCollectionConverter", "RuleElementCollectionConverter is not specified");

            this.directoriesConvertionService = directoriesConvertionService ??
                throw new System.ArgumentNullException("WatchedDirectoryElementCollectionConverter", "WatchedDirectoryElementCollectionConverter is not specified");
        }

        public SystemWatcher Convert(SystemWatcherConfigurationSection source)
        {
            SystemWatcher systemWatcher = new SystemWatcher();

            systemWatcher.Culture = source.CultureInfo.CurrentCulture;
            systemWatcher.Rules = this.rulesConvertionService.Convert(source.Rules);
            systemWatcher.WatchedDirectories = this.directoriesConvertionService.Convert(source.WatchedDirectories);

            return systemWatcher;
        }
    }
}
