using System.Configuration;

namespace SystemWatcherSolution.Services.Converting
{
    public interface IElementConverter<S, D>
        where S : ConfigurationElement 
        where D : class
    {
        /// <summary>
        /// Converts passed element paramater from configuration section to corresponding model
        /// </summary>
        /// <param name="source">Element from configuration section</param>
        /// <returns>Corresponding model</returns>
        D Convert(S source);
    }
}
