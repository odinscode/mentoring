using System.Collections.Generic;
using SystemWatcherSolution.Models.Configuration;
using SystemWatcherSolution.Models.Entities;

namespace SystemWatcherSolution.Services.Converting
{
    public class RuleElementCollectionConverter : IElementConverter<RuleElementCollection, IEnumerable<Rule>>
    {
        private readonly IElementConverter<RuleElement, Rule> ruleConvertionService;

        public RuleElementCollectionConverter(IElementConverter<RuleElement, Rule> convertionService)
        {
            this.ruleConvertionService = convertionService ??
                throw new System.ArgumentNullException("RuleElementCollectionConverter", "RuleElementConverter is not specified");
        }

        public IEnumerable<Rule> Convert(RuleElementCollection source)
        {
            var rules = new List<Rule>();

            foreach (RuleElement ruleElement in source)
            {
                var rule = this.ruleConvertionService.Convert(ruleElement);
                rules.Add(rule);
            }

            return rules;
        }
    }
}
