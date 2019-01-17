using CustomReflectionLibrary.Services;
using System.Reflection;
using CustomReflectionLibrary.Helpers;

namespace ReflectionSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.AddAssemlby(Assembly.GetExecutingAssembly());

            var executingAssemlby = Assembly.GetExecutingAssembly();

            var types = System.AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(Program));

        }
    }
}
