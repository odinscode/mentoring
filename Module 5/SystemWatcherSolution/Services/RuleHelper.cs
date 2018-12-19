using System.Collections.Generic;
using System.Linq;
using SystemWatcherSolution.Models.Entities;
using SystemWatcherSolution.Models.EventArgs;

namespace SystemWatcherSolution.Services
{
    public static class RuleHelper
    {
        public static List<RuleHelperModel> trackedRules = new List<RuleHelperModel>();

        public static void OnRuleMatched(object sender, RuleEventArgs e)
        {
            var customFileSystemWatcher = sender as CustomFileSystemWatcher;
            if (customFileSystemWatcher != null) 
            {
                if (IsWatchedDirectoryAdded(customFileSystemWatcher.Path))
                {

                }
                else
                {
                    
                }
            }
        }

        public static bool IsWatchedDirectoryAdded(string path)
        {
            return trackedRules.Any(r => r.WatchedDirectory == path);
        }
    }

    public class RuleHelperModel
    {
        public string WatchedDirectory { get; set; }

        // todo: rethink about using list of rules
        public Rule Rule { get; set; }

        public int ElementsAffected { get; set; }
    }
}
