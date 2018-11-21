using System.Configuration;

namespace SystemWatcherSolution.Models.Configuration
{
    public class RuleElementCollection : ConfigurationElementCollection
    {
        private const string DefaultDirectoryPathAttribute = "defaultDirectoryPath";

        [ConfigurationProperty(DefaultDirectoryPathAttribute, IsRequired = true)]
        public string DefaultDirectoryPath
        {
            get { return (string)base[DefaultDirectoryPathAttribute]; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).Regex;
        }
    }
}
