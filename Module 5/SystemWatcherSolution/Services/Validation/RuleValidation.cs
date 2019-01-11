using SystemWatcherSolution.Models.Configuration;
using System.IO;

namespace SystemWatcherSolution.Services.Validation
{
    public class RuleValidation : IElementValidation<RuleElement>
    {
        public void CheckValidity(RuleElement validatable)
        {
            if (validatable == null)
                throw new System.ArgumentNullException(Constants.RuleElement, Resources.Resources.RuleIsNotSetException);

            if (string.IsNullOrWhiteSpace(validatable.Regex))
                throw new System.ArgumentNullException(Constants.RegexElement, Resources.Resources.RegularExpressionIsEmptyException);

            if (!Directory.Exists(validatable.TargetPath))
                throw new DirectoryNotFoundException(string.Format(Resources.Resources.DirectoryNotFoundException, validatable.TargetPath));
        }
    }
}
