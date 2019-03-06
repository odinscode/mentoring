namespace SystemWatcherSolution.Models.EventArgs
{
    using System;
    using SystemWatcherSolution.Models.Entities;

    public class RuleEventArgs : EventArgs
    {
        public Rule Rule { get; set; }

        public string FileName { get; set; }

        public RuleEventArgs(Rule rule, string fileName)
        {
            this.Rule = rule;
            this.FileName = fileName;
        }
    }
}
