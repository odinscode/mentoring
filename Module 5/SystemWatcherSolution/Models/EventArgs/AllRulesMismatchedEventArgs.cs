namespace SystemWatcherSolution.Models.EventArgs
{
    using System;

    public class AllRulesMismatchedEventArgs : EventArgs
    {
        public string FileName { get; set; }

        public string DefaultDirectoryPath { get; set; }

        public AllRulesMismatchedEventArgs(string fileName ,string defaultDirectoryPath)
        {
            this.FileName = fileName;
            this.DefaultDirectoryPath = defaultDirectoryPath;
        }
    }
}
