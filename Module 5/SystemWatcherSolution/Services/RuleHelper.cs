using System;
using System.Collections.Generic;
using System.Linq;
using SystemWatcherSolution.Models.Entities;

namespace SystemWatcherSolution.Services
{
    public class RuleHelperModel
    {
        public string WatchedDirectory { get; set; }

        // todo: use at dictionary string == name of regex as a key parameter
        public Dictionary<Rule, int> Rules { get; set; }
    }

    public static class RuleHelper
    {
        private static object lockObject = new object();

        public static List<RuleHelperModel> TrackedDirectoriesAndRules = new List<RuleHelperModel>();

        public static string UpdateFileName(string fileName, Rule rule, string watchedDirectoryPath)
        {
            if (IsOrderNumberRequired(rule))
            {
                lock (lockObject)
                {
                    fileName = AddOrderNumberToFileName(fileName, rule, watchedDirectoryPath);
                }
            }
            // todo: finish moveDate logic
            //if (IsMoveDateRequired(rule))
            //{
            //    modifiableFileName = AddMoveDateToFileName(fileName, rule, watchedDirectoryPath);
            //}

            return fileName;
        }

        private static string AddMoveDateToFileName(string fileName, Rule rule, string watchedDirectoryPath)
        {
            throw new NotImplementedException();
        }

        private static string AddOrderNumberToFileName(string fileName, Rule rule, string watchedDirectoryPath)
        {
            if (!IsWatchedDirectoryAdded(watchedDirectoryPath))
                AddWatchedDirectoryAndRule(watchedDirectoryPath, rule);
            if (!IsRuleAdded(watchedDirectoryPath, rule))
            {
                //AddRuleToWatchedDirectory(watchedDirectoryPath, rule);
            }
            // todo: if directory was already added but not the rule - add that logic

            var updatedOrderNumber = GetUpdatedOrderNumberForRule(watchedDirectoryPath, rule);

            return $"{updatedOrderNumber}) {fileName}";
        }

        private static bool IsRuleAdded(string watchedDirectoryPath, Rule rule)
        {
            throw new NotImplementedException();
        }

        private static bool IsMoveDateRequired(Rule rule)
        {
            return rule.IsMovedDateRequired;
        }

        private static bool IsOrderNumberRequired(Rule rule)
        {
            return rule.IsOrderNumberRequired;
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
                    .Rules
                    .FirstOrDefault(r => r.Key.Name == rule.Name);

            TrackedDirectoriesAndRules
                .FirstOrDefault(d => d.WatchedDirectory == watchedDirectoryPath)
                .Rules[storedRule.Key] = ++previousOrderNumber;
        }

        private static int GetCurrentOrderNumberForRule(string watchedDirectoryPath, Rule rule)
        {
            return TrackedDirectoriesAndRules
                    .FirstOrDefault(d => d.WatchedDirectory == watchedDirectoryPath)
                    .Rules
                    .FirstOrDefault(r => r.Key.Name == rule.Name)
                    .Value;
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
                Rules = new Dictionary<Rule, int>()
                {
                    { rule, 0 }
                }
            };
        }
    }
}
