using System.Configuration;

namespace SystemWatcherSolution.Models.Configuration
{
    public class WatchedDirectoryElement : ConfigurationElement
    {
        private const string PathAttribute = "path";

        [ConfigurationProperty(PathAttribute, IsKey = true)]
        public string DirectoryPath
        {
            get { return (string)base[PathAttribute]; }
        }
    }
}
