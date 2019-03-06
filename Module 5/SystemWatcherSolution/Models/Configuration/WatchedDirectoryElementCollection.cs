using System.Configuration;

namespace SystemWatcherSolution.Models.Configuration
{
    public class WatchedDirectoryElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new WatchedDirectoryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WatchedDirectoryElement)element).DirectoryPath;
        }
    }
}
