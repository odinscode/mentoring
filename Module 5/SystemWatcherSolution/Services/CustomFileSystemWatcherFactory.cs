using System.Collections.Generic;
using SystemWatcherSolution.Models.Entities;

namespace SystemWatcherSolution.Services
{
    public class CustomFileSystemWatcherFactory : ICustomFileSystemWatcherFactory
    {
        public CustomFileSystemWatcher CreateInstance(string directoryPath, List<Rule> rules)
        {
            return new CustomFileSystemWatcher(directoryPath, rules);
        }
    }
}
