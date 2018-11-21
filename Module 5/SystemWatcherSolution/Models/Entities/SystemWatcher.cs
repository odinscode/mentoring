using System.Collections.Generic;
using System.Globalization;

namespace SystemWatcherSolution.Models.Entities
{
    public class SystemWatcher
    {
        public CultureInfo Culture { get; set; }

        public IEnumerable<WatchedDirectory> WatchedDirectories { get; set; }

        public IEnumerable<Rule> Rules { get; set; }
    }
}
