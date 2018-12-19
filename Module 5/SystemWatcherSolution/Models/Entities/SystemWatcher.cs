using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SystemWatcherSolution.Models.Entities
{
    public class SystemWatcher
    {
        public CultureInfo Culture { get; set; }

        public IEnumerable<WatchedDirectory> WatchedDirectories { get; set; }

        public IEnumerable<Rule> Rules { get; set; }

        public DirectoryInfo DefaultDirectory { get; set; }
    }
}
