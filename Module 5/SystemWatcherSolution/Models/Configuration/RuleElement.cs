﻿using System.Configuration;

namespace SystemWatcherSolution.Models.Configuration
{
    public class RuleElement : ConfigurationElement
    {
        private const string NameAttribute = "name";
        private const string RegexAttribute = "regex";
        private const string TargetPathAttribute = "targetDirectoryPath";
        private const string IsOrderNumberRequiredAttribute = "isOrderNumberRequired";
        private const string IsMovedTimeRequiredAttribute = "isMovedTimeRequired";

        [ConfigurationProperty(NameAttribute, IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base[NameAttribute]; }
        }

        [ConfigurationProperty(RegexAttribute)]
        public string Regex
        {
            get { return (string)base[RegexAttribute]; }
        }

        [ConfigurationProperty(TargetPathAttribute)]
        public string TargetPath
        {
            get { return (string)base[TargetPathAttribute]; }
        }

        [ConfigurationProperty(IsOrderNumberRequiredAttribute, DefaultValue = "false", IsRequired = false)]
        public bool IsOrderNumberRequired
        {
            get { return (bool)base[IsOrderNumberRequiredAttribute]; }
        }

        [ConfigurationProperty(IsMovedTimeRequiredAttribute, DefaultValue = "false", IsRequired = false)]
        public bool IsMovedTimeRequired
        {
            get { return (bool)base[IsMovedTimeRequiredAttribute]; }
        }
    }
}
