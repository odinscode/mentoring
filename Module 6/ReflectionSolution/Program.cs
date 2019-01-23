using CustomReflectionLibrary.Services;
using System.Reflection;
using CustomReflectionLibrary.Helpers;
using ReflectionSolution.BLL.Models;
using System.Linq;
using System;

namespace ReflectionSolution
{
    class Program
    {
        private static Container _container;

        static void Main(string[] args)
        {
            InitializeNecessaryDependecies();

            _container = new Container();
            SetupContainer(_container);

            var customerCtor = _container.CreateInstance<ReflectionSolution.BLL.Models.CustomerCtor>();
            var customerProp = _container.CreateInstance<ReflectionSolution.BLL.Models.CustomerProp>();

            var test = (ReflectionSolution.BLL.Models.CustomerCtor)_container.CreateInstance(typeof(ReflectionSolution.BLL.Models.CustomerCtor));

            var types = System.AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(Program));

            ShowTypesInfo(types);
        }

        private static void InitializeNecessaryDependecies()
        {
            // Without specifying type from different project(assembly) - 
            // referenced projects (as their types) will not be added in 
            // reflection output
            // TODO: Remove reference to DAL - first parameter for CustomerCtor constructor
            var customerBll = new CustomerCtor(null, null);
        }

        private static void SetupContainer(Container container)
        {
            // Not just that assembly will be used for creating types
            container.AddAssemlby(Assembly.GetExecutingAssembly());
            container.AddType(typeof(ReflectionSolution.DAL.Models.Customer), typeof(ReflectionSolution.DAL.Models.ICustomer));
            container.AddType(typeof(ReflectionSolution.DAL.Services.Logger));
        }

        //private static void CheckContainerResolving()

        private static void ShowTypesInfo(Type[] types)
        {
            foreach (var type in types)
            {
                Console.WriteLine("Type: " + type.Name);
                Console.WriteLine("Custom attributes on type: ");
                foreach (var customAttribute in type.CustomAttributes)
                {
                    Console.WriteLine(customAttribute.AttributeType.Name);
                }
                Console.WriteLine("Properties of type: ");
                foreach (var property in type.GetProperties())
                {
                    Console.WriteLine("Property: " + property.Name);
                    Console.WriteLine("Custom attributes on property: ");
                    foreach (var customAttribute in property.GetCustomAttributes())
                    {
                        Console.WriteLine(customAttribute.TypeId);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
