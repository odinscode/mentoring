using System.Configuration;

namespace SystemWatcherSolution.Models.Configuration
{
    public class SystemWatcherConfigurationSection : ConfigurationSection
    {
        private const string CultureInfoAttribure = "cultureInfo";
        private const string WatchedDirectoriesAttribure = "watchedDirectories";
        private const string RulesAttribute = "rules";

        [ConfigurationProperty(CultureInfoAttribure)]
        public CultureInfoElement CultureInfo
        {
            get { return (CultureInfoElement)this[CultureInfoAttribure]; }
        }

        [ConfigurationProperty(WatchedDirectoriesAttribure)]
        public WatchedDirectoryElementCollection WatchedDirectories
        {
            get { return (WatchedDirectoryElementCollection)this[WatchedDirectoriesAttribure]; }
        }

        [ConfigurationProperty(RulesAttribute)]
        public RuleElementCollection Rules
        {
            get { return (RuleElementCollection)this[RulesAttribute]; }
        }
    }
}
