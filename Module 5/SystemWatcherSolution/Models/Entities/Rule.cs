using System.IO;
using System.Text.RegularExpressions;

namespace SystemWatcherSolution.Models.Entities
{
    public class Rule
    {
        public string Name { get; set; }

        public Regex Regex { get; set; }

        public DirectoryInfo TargetDirectory { get; set; }

        public bool IsOrderNumberRequired { get; set; }

        public bool IsMovedDateRequired { get; set; }
    }
}
