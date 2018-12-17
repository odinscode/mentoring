using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemWatcherSolution.Models.Entities;
using SystemWatcherSolution.Models.EventArgs;
using SystemWatcherSolution.Models.EventHandlers;

namespace SystemWatcherSolution.Services
{
    public class CustomFileSystemWatcher : FileSystemWatcher
    {
        public List<Rule> Rules { get; set; }

        public event RuleEventHandler RuleMatched;
        public event EventHandler<RuleEventArgs> RuleMismatched;

        public CustomFileSystemWatcher(string path) : base(path)
        {
            this.Rules = new List<Rule>();
            this.Filter = "*.*";
            this.EnableRaisingEvents = true;

            var notifyFilters = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                 | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            this.NotifyFilter = notifyFilters;

            SetupEvents();
        }

        public CustomFileSystemWatcher(string path, Rule pattern) : this(path)
        {
            if (!IsPatternAdded(pattern))
                this.Rules.Add(pattern);
        }

        public CustomFileSystemWatcher(string path, List<Rule> patterns) : this(path)
        {
            foreach (var pattern in patterns)
            {
                if (!IsPatternAdded(pattern))
                    this.Rules.Add(pattern);
            }
        }

        private bool IsPatternAdded(Rule rule)
        {
            return 
                this.Rules.FirstOrDefault(p => p.Regex == rule.Regex) == null
                    ? false
                    : true;
        }

        private void SetupEvents()
        {
            this.RuleMatched += new RuleEventHandler(OnRuleMathced);
            this.RuleMismatched += new EventHandler<RuleEventArgs>(OnRuleMismatched);
            this.Changed += new FileSystemEventHandler(OnChanged);
            this.Created += new FileSystemEventHandler(OnChanged);
            this.Deleted += new FileSystemEventHandler(OnChanged);
            this.Renamed += new RenamedEventHandler(OnRenamed);
        }

        private void OnRuleMathced(object sender, RuleEventArgs e)
        {
            Console.WriteLine($"{e.Rule} is matched");
        }

        private void OnRuleMismatched(object sender, RuleEventArgs e)
        {
            Console.WriteLine($"{e.Rule} is not matched");
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            CheckRuleMatching(e.Name);
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            CheckRuleMatching(e.Name);
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }

        private void CheckRuleMatching(string ItemName)
        {
            // todo: localize ChangeType values
            foreach (var pattern in this.Rules)
            {
                if (pattern.Regex.IsMatch(ItemName))
                    RuleMatched?.Invoke(this, new RuleEventArgs(pattern.Regex.ToString()));
                else
                    RuleMismatched?.Invoke(this, new RuleEventArgs(pattern.Regex.ToString()));
            }
        }
    }
}