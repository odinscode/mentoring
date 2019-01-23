using System.Collections.Generic;

namespace CustomReflectionLibrary.Helpers
{
    public static class ReflectionHelpers
    {
        public static System.Type[] GetAllDerivedTypes(this System.AppDomain aAppDomain, System.Type aType)
        {
            var result = new List<System.Type>();

            var assemblies = aAppDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (FilterCustomProjects(type.Namespace))
                        result.Add(type);
                }
            }

            return result.ToArray();
        }

        private static bool FilterCustomProjects(string @namespace)
        {
            return @namespace != null
                        && (@namespace.Contains("ReflectionSolution")
                        || @namespace.Contains("CustomReflection"));
        }
    }
}
