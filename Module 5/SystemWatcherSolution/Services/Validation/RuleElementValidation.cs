using SystemWatcherSolution.Models.Configuration;
using System.IO;

namespace SystemWatcherSolution.Services.Validation
{
    public class RuleElementValidation : IElementValidation<RuleElement>
    {
        public void CheckValidity(RuleElement validatable)
        {
            if (validatable == null)
                throw new System.ArgumentNullException("ruleElement", Resources.Exceptions.RuleIsNotSet);

            if (string.IsNullOrWhiteSpace(validatable.Regex))
                throw new System.ArgumentNullException("regex", Resources.Exceptions.RegularExpressionIsEmpty);

            if (!Directory.Exists(validatable.TargetPath))
                throw new DirectoryNotFoundException(string.Format(Resources.Exceptions.DirtectoryNotFound, validatable.TargetPath));
        }
    }
}
