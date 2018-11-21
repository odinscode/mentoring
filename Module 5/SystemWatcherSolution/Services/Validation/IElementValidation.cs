using System.Configuration;

namespace SystemWatcherSolution.Services.Validation
{
    public interface IElementValidation<T>
        where T: ConfigurationElement
    {
        /// <summary>
        /// Makes passes parameter validation
        /// </summary>
        /// <param name="validatable">Object which requires validation</param>
        void CheckValidity(T validatable);
    }
}
