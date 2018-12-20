namespace SystemWatcherSolution.Models.EventArgs
{
    using System;
    using SystemWatcherSolution.Models.Entities;

    public class RuleEventArgs : EventArgs
    {
        public Rule Rule { get; set; }

        public RuleEventArgs(Rule rule)
        {
            this.Rule = rule;
        }
    }
}
