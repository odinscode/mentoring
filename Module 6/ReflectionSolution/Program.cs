using CustomReflectionLibrary.Services;
using System.Reflection;
using CustomReflectionLibrary.Helpers;
using System.Linq;
using ReflectionSolution.BLL.Models;

namespace ReflectionSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.AddAssemlby(Assembly.GetExecutingAssembly());

            //var customerBll = new CustomerCtor(null, null);

            var executingAssemlby = Assembly.GetExecutingAssembly();

            var types = System.AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(Program));
        }
    }
}
