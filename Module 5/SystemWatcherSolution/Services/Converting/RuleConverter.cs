using System.IO;
using System.Text.RegularExpressions;
using SystemWatcherSolution.Models.Configuration;
using SystemWatcherSolution.Models.Entities;
using SystemWatcherSolution.Services.Validation;

namespace SystemWatcherSolution.Services.Converting
{
    public class RuleConverter : IElementConverter<RuleElement, Rule>
    {
        private readonly IElementValidation<RuleElement> validationService;

        public RuleConverter(IElementValidation<RuleElement> validationService)
        {
            this.validationService = validationService;
        }

        public Rule Convert(RuleElement source)
        {
            validationService.CheckValidity(source);

            Rule rule = new Rule()
            {
                Name = source.Name,
                Regex = new Regex(source.Regex),
                TargetDirectory = new DirectoryInfo(source.TargetPath),
                IsOrderNumberRequired = source.IsOrderNumberRequired,
                IsMovedTimeRequired = source.IsMovedTimeRequired
            };

            return rule;
        }
    }
}
