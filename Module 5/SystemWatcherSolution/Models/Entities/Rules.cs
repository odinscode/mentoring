using System.Collections;
using System.Collections.Generic;

namespace SystemWatcherSolution.Models.Entities
{
    // Todo: investigate if that class is nessecary
    public class Rules : IEnumerable<Rule>
    {
        private List<Rule> rules = new List<Rule>();

        public Rule this[int index]
        {
            get { return rules[index]; }
            set { rules.Insert(index, value); }
        }

        public IEnumerator<Rule> GetEnumerator()
        {
            // return rules.GetEnumerator();
            foreach (Rule rule in rules)
                yield return rule;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
