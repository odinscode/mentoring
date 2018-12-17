using System.Collections.Generic;
using SystemWatcherSolution.Models.Entities;

namespace SystemWatcherSolution.Services
{
    public interface ICustomFileSystemWatcherFactory
    {
        CustomFileSystemWatcher CreateInstance(string directoryPath, List<Rule> rules);
    }
}
