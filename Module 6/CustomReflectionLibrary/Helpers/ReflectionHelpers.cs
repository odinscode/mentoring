using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomReflectionLibrary.Helpers
{
    public static class ReflectionHelpers
    {
        public static System.Type[] GetAllDerivedTypes(this System.AppDomain aAppDomain, System.Type aType)
        {
            var result = new List<System.Type>();

            var moduleScopeNamesAndAttributes = new List<string>();

            var assemblies = aAppDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.Namespace != null
                        && (type.Namespace.Contains("ReflectionSolution")
                        || type.Namespace.Contains("CustomReflection")))
                    {
                        result.Add(type);
                    }

                    var customAttributes = type.CustomAttributes.Select(_ => _.AttributeType.Name).ToList();
                    moduleScopeNamesAndAttributes.Add($"{type.FullName} : {customAttributes.Select(_ => _ + " ")}");
                }
            }

            var temp = moduleScopeNamesAndAttributes.Where(a => a.Contains("Import") || a.Contains("Export")).ToList();

            return result.ToArray();
        }
    }
}
