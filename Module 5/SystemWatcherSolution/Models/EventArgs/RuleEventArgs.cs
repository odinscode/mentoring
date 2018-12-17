namespace SystemWatcherSolution.Models.EventArgs
{
    using System;

    public class RuleEventArgs : EventArgs
    {
        public string Rule { get; set; }

        public RuleEventArgs(string rule)
        {
            this.Rule = rule;
        }
    }
}
