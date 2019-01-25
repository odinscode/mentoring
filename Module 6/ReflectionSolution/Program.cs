using CustomReflectionLibrary.Services;
using ReflectionSolution.BLL.Models;
using System;
using System.Collections.Generic;

namespace ReflectionSolution
{
    class Program
    {
        private static Container _container;

        static void Main(string[] args)
        {
            InitializeNecessaryDependecies();

            _container = new Container(GetCustomResolvingDependenciesImportAttributes(), GetCustomResolvingDependenciesExportAttributes());
            SetupContainer();
            CheckContainerResolvingDependencies();
        }

        private static void InitializeNecessaryDependecies()
        {
            // Without specifying type from different project(assembly) - 
            // referenced projects (as their types) will not be added in 
            // reflection output
            // TODO: Remove reference to DAL - first parameter for CustomerCtor constructor
            var customerBll = new CustomerCtor(null, null);
        }

        private static IEnumerable<Type> GetCustomResolvingDependenciesImportAttributes()
        {
            return new List<Type>()
            {
                typeof(CustomReflectionLibrary.Attributes.ImportAttribute),
                typeof(CustomReflectionLibrary.Attributes.ImportConstructorAttribute)
            };
        }

        private static IEnumerable<Type> GetCustomResolvingDependenciesExportAttributes()
        {
            return new List<Type>()
            {
                typeof(CustomReflectionLibrary.Attributes.ExportAttribute)
            };
        }

        private static void SetupContainer()
        {
            // Not just that assembly will be used for creating types
            //container.AddAssemlby(Assembly.GetExecutingAssembly());

            //_container.AddType(typeof(ReflectionSolution.DAL.Models.Customer), typeof(ReflectionSolution.DAL.Models.ICustomer));
            _container.AddType(typeof(ReflectionSolution.BLL.Models.CustomerCtor));
            _container.AddType(typeof(ReflectionSolution.DAL.Services.Logger));
        }

        private static void CheckContainerResolvingDependencies()
        {
            var customerCtor = (ReflectionSolution.BLL.Models.CustomerCtor)_container.CreateInstance(typeof(ReflectionSolution.BLL.Models.CustomerCtor));
            //var customerProp = _container.CreateInstance<ReflectionSolution.BLL.Models.CustomerProp>();
        }
    }
}
