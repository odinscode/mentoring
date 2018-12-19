using System.Collections.Generic;
using System.IO;
using SystemWatcherSolution.Models.Entities;

namespace SystemWatcherSolution.Services
{
    public class CustomFileSystemWatcherFactory
    {
        public CustomFileSystemWatcher CreateInstance(string monitoringDirectoryPath, List<Rule> rules, DirectoryInfo defaultDirectory)
        {
            return new CustomFileSystemWatcher(monitoringDirectoryPath, rules, defaultDirectory);
        }
    }
}
