using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemWatcherSolution.Models.Entities;
using SystemWatcherSolution.Models.EventArgs;

namespace SystemWatcherSolution.Services
{
    public class CustomFileSystemWatcher : FileSystemWatcher
    {
        private object lockObject = new object();

        public DirectoryInfo DefaultDirectory { get; set; }
        public List<Rule> Rules { get; set; }

        public event EventHandler<RuleEventArgs> RuleMatched;
        public event EventHandler<RuleEventArgs> RuleMismatched;
        public event EventHandler<AllRulesMismatchedEventArgs> AllRulesMismathced;

        public CustomFileSystemWatcher(string monitoringDirectoryPath, DirectoryInfo DefaultDirectory) : base(monitoringDirectoryPath)
        {
            this.Rules = new List<Rule>();
            this.Filter = "*.*";
            this.EnableRaisingEvents = true;

            var notifyFilters = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                 | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            this.NotifyFilter = notifyFilters;

            SetupEvents();
        }

        public CustomFileSystemWatcher(string monitoringDirectoryPath, List<Rule> rules, DirectoryInfo DefaultDirectory) : this(monitoringDirectoryPath, DefaultDirectory)
        {
            foreach (var rule in rules)
            {
                if (!IsRuleAdded(rule))
                    this.Rules.Add(rule);
            }
        }

        private bool IsRuleAdded(Rule rule)
        {
            return this.Rules.Any(p => p.Regex == rule.Regex);
        }

        private void SetupEvents()
        {
            this.RuleMatched += new EventHandler<RuleEventArgs>(OnRuleMathced);
            this.RuleMismatched += new EventHandler<RuleEventArgs>(OnRuleMismatched);
            this.AllRulesMismathced += new EventHandler<AllRulesMismatchedEventArgs>(OnAllRulesMismatched);
            this.Changed += new FileSystemEventHandler(OnChanged);
            this.Created += new FileSystemEventHandler(OnCreated);
            this.Deleted += new FileSystemEventHandler(OnDeleted);
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

        private void OnAllRulesMismatched(object sender, AllRulesMismatchedEventArgs e)
        {
            Console.WriteLine($"All rules mismached on {e.FileName}, file will be moved to {e.DefaultDirectoryPath}");
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            CheckRuleMatchingAndMoveFile(e.FullPath);
            Console.WriteLine($"File: {e.FullPath} changed");
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            CheckRuleMatchingAndMoveFile(e.FullPath);
            Console.WriteLine($"File: {e.FullPath} created");
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File: {e.FullPath} deleted");
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            CheckRuleMatchingAndMoveFile(e.FullPath);
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }

        private void CheckRuleMatchingAndMoveFile(string sourceFileName)
        {
            var originalFileName = System.IO.Path.GetFileName(sourceFileName);

            bool isAnyRuleMatched = false;

            foreach (var rule in this.Rules)
            {
                string modifiableFileName = string.Copy(originalFileName);

                if (rule.Regex.IsMatch(originalFileName))
                {
                    isAnyRuleMatched = true;
                    RuleMatched?.Invoke(this, new RuleEventArgs(rule.Regex.ToString()));
                    // todo: use naming conventions as specified in task (creation date and order number)
                    if (rule.IsOrderNumberRequired)
                    {
                        // singleton class for handling orderNumber counting
                    }

                    var destinationFileName = $@"{rule.TargetDirectory.FullName}\{modifiableFileName}";
                    CheckFileExistanceAndCopyToDestination(sourceFileName, destinationFileName);
                }
                else
                    RuleMismatched?.Invoke(this, new RuleEventArgs(rule.Regex.ToString()));
            }

            if (!isAnyRuleMatched)
            {
                AllRulesMismathced?.Invoke(this, new AllRulesMismatchedEventArgs(sourceFileName, this.DefaultDirectory.FullName));

                var destinationPath = $@"{this.DefaultDirectory.FullName}\{originalFileName}";
                CheckFileExistanceAndCopyToDestination(sourceFileName, destinationPath);
            }

            File.Delete(sourceFileName);
        }

        private void CheckFileExistanceAndCopyToDestination(string sourceFileName, string destinationFileName)
        {
            lock (this.lockObject)
            {
                if (File.Exists(sourceFileName))
                    File.Copy(sourceFileName, destinationFileName, true);
            }
        }
    }
}