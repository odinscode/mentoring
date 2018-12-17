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

        public CustomFileSystemWatcher(string path, Rule rule) : this(path)
        {
            if (!IsRuleAdded(rule))
                this.Rules.Add(rule);
        }

        public CustomFileSystemWatcher(string path, List<Rule> rules) : this(path)
        {
            foreach (var rule in rules)
            {
                if (!IsRuleAdded(rule))
                    this.Rules.Add(rule);
            }
        }

        private bool IsRuleAdded(Rule rule)
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
            CheckRuleMatchingAndMoveFile(e.FullPath);
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            CheckRuleMatchingAndMoveFile(e.FullPath);
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }

        private void CheckRuleMatchingAndMoveFile(string fullPath)
        {
            var fileName = System.IO.Path.GetFileName(fullPath);

            // todo: localize ChangeType values
            foreach (var rule in this.Rules)
            {
                if (rule.Regex.IsMatch(fileName))
                {
                    RuleMatched?.Invoke(this, new RuleEventArgs(rule.Regex.ToString()));
                    // todo: use naming conventions as specified in task (creation date and order number)
                    var destinationPath = $@"{rule.TargetDirectory.FullName}\{fileName}";
                    // todo: check if destination folder already have file inside of it
                    System.IO.File.Copy(fullPath, destinationPath);
                }
                else
                {
                    RuleMismatched?.Invoke(this, new RuleEventArgs(rule.Regex.ToString()));
                    // todo: move file to default folder which is not stored in rule section
                }
            }

            // todo: file must be deleted after been copied
        }
    }
}