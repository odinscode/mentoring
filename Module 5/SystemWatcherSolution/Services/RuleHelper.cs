using System;
using System.Collections.Generic;
using System.Linq;
using SystemWatcherSolution.Models.Entities;

namespace SystemWatcherSolution.Services
{
    public class RuleHelperModel
    {
        public string WatchedDirectory { get; set; }

        public Dictionary<string, int> RulesCounter { get; set; }
    }

    public static class RuleHelper
    {
        private static object lockObject = new object();

        public static List<RuleHelperModel> TrackedDirectoriesAndRules = new List<RuleHelperModel>();

        public static string UpdateFileName(string fileName, Rule rule, string watchedDirectoryPath)
        {
            if (rule.IsOrderNumberRequired)
            {
                lock (lockObject)
                {
                    fileName = AddOrderNumberToFileName(fileName, rule, watchedDirectoryPath);
                }
            }
            if (rule.IsMovedTimeRequired)
            {
                lock (lockObject)
                {
                    fileName = AddMoveTimeToFileName(fileName, rule, watchedDirectoryPath);
                }
            }

            return fileName;
        }

        private static string AddOrderNumberToFileName(string fileName, Rule rule, string watchedDirectoryPath)
        {
            EnsureDirectoryAndRuleAdded(watchedDirectoryPath, rule);

            int updatedOrderNumber = GetUpdatedOrderNumberForRule(watchedDirectoryPath, rule);

            var fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
            var fileExtension = System.IO.Path.GetExtension(fileName);

            return $"{fileNameWithoutExtension} ({updatedOrderNumber}){fileExtension}";
        }

        private static string AddMoveTimeToFileName(string fileName, Rule rule, string watchedDirectoryPath)
        {
            EnsureDirectoryAndRuleAdded(watchedDirectoryPath, rule);

            string updatedMoveTime = GetUpdatedMoveTimeForRule(watchedDirectoryPath, rule);

            var fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
            var fileExtension = System.IO.Path.GetExtension(fileName);

            return $"{fileNameWithoutExtension} {updatedMoveTime}{fileExtension}";
        }

        private static void EnsureDirectoryAndRuleAdded(string watchedDirectoryPath, Rule rule)
        {
            EnsureDirectoryAdded(watchedDirectoryPath, rule);
            EnsureRuleAdded(watchedDirectoryPath, rule);
        }

        private static void EnsureDirectoryAdded(string watchedDirectoryPath, Rule rule)
        {
            if (!IsWatchedDirectoryAdded(watchedDirectoryPath))
                AddWatchedDirectoryAndRule(watchedDirectoryPath, rule);
        }

        private static void EnsureRuleAdded(string watchedDirectoryPath, Rule rule)
        {
            if (!IsRuleAdded(watchedDirectoryPath, rule))
                AddRuleToWatchedDirectory(watchedDirectoryPath, rule);
        }

        private static bool IsWatchedDirectoryAdded(string watchedDirectoryPath)
        {
            return TrackedDirectoriesAndRules.Any(r => r.WatchedDirectory == watchedDirectoryPath);
        }

        private static void AddWatchedDirectoryAndRule(string watchedDirectoryPath, Rule rule)
        {
            var ruleHelper = new RuleHelperModel()
            {
                WatchedDirectory = watchedDirectoryPath,
                RulesCounter = new Dictionary<string, int>()
                {
                    { rule.Name, 0 }
                }
            };

            TrackedDirectoriesAndRules.Add(ruleHelper);
        }

        private static bool IsRuleAdded(string watchedDirectoryPath, Rule rule)
        {
            return TrackedDirectoriesAndRules
                .FirstOrDefault(d => d.WatchedDirectory == watchedDirectoryPath)
                .RulesCounter
                .Keys
                .Any(k => k == rule.Name);
        }

        private static void AddRuleToWatchedDirectory(string watchedDirectoryPath, Rule rule)
        {
            TrackedDirectoriesAndRules
                .FirstOrDefault(d => d.WatchedDirectory == watchedDirectoryPath)
                .RulesCounter
                .Add(rule.Name, 0);
        }

        private static int GetUpdatedOrderNumberForRule(string watchedDirectoryPath, Rule rule)
        {
            int currentPosition = GetCurrentOrderNumberForRule(watchedDirectoryPath, rule);
            UpdateCurrentOrderNumberForRule(watchedDirectoryPath, rule, currentPosition);

            return GetCurrentOrderNumberForRule(watchedDirectoryPath, rule);
        }

        private static void UpdateCurrentOrderNumberForRule(string watchedDirectoryPath, Rule rule, int previousOrderNumber)
        {
            var storedRule = TrackedDirectoriesAndRules
                .FirstOrDefault(d => d.WatchedDirectory == watchedDirectoryPath)
                    .RulesCounter
                    .FirstOrDefault(r => r.Key == rule.Name);

            TrackedDirectoriesAndRules
                .FirstOrDefault(d => d.WatchedDirectory == watchedDirectoryPath)
                .RulesCounter[storedRule.Key] = ++previousOrderNumber;
        }

        private static int GetCurrentOrderNumberForRule(string watchedDirectoryPath, Rule rule)
        {
            return TrackedDirectoriesAndRules
                    .FirstOrDefault(d => d.WatchedDirectory == watchedDirectoryPath)
                    .RulesCounter
                    .FirstOrDefault(r => r.Key == rule.Name)
                    .Value;
        }

        private static string GetUpdatedMoveTimeForRule(string watchedDirectoryPath, Rule rule)
        {
            // We can't specify time in format "HH:mm:ss" due to window's blocking ':'
            // char in file naming
            return DateTime.Now.ToString("HH-mm-ss");
        }
    }
}
