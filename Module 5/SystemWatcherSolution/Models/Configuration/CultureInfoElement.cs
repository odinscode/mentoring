using System.Configuration;
using System.Globalization;

namespace SystemWatcherSolution.Models.Configuration
{
    public class CultureInfoElement : ConfigurationElement
    {
        private const string NameAttribute = "name";

        [ConfigurationProperty(NameAttribute)]
        public CultureInfo CurrentCulture
        {
            get { return (CultureInfo)base[NameAttribute]; }
        }
    }
}
