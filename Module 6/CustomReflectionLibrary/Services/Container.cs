using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomReflectionLibrary.Services
{
    public class Container
    {
        private Dictionary<string, Type> addedTypes;

        private List<Type> resolvingDependeciesImportAttributes;

        private List<Type> resolvingDependeciesExportAttributes;

        public Container()
        {
            this.addedTypes = new Dictionary<string, Type>();
            this.resolvingDependeciesImportAttributes = new List<Type>();
            this.resolvingDependeciesExportAttributes = new List<Type>();
        }

        public Container(IEnumerable<Type> resolvingDependeciesImportAttributes, IEnumerable<Type> resolvingDependeciesExportAttributes) : this()
        {
            this.resolvingDependeciesImportAttributes = new List<Type>(resolvingDependeciesImportAttributes);
            this.resolvingDependeciesExportAttributes = new List<Type>(resolvingDependeciesExportAttributes);
        }

        public void AddType(Type type)
        {
            if (!addedTypes.ContainsKey(type.FullName))
                addedTypes.Add(type.FullName, type);
        }

        public void AddType(Type type, Type baseType)
        {
            // TODO: change dictionary "Value" type - instead of Type use some custom 
            // entity for storing not just passing type, but also it's base type

            if (!addedTypes.ContainsKey(baseType.FullName))
                addedTypes.Add(baseType.FullName, type);
        }

        public object CreateInstance(Type type)
        {
            if (!addedTypes.ContainsKey(type.FullName))
                throw new Exception($"{type.FullName} is not added to container");

            if (type.CustomAttributes.Any())
            {
                if (type.CustomAttributes
                    .Any(_ => _.AttributeType == typeof(Attributes.ImportConstructorAttribute)))
                {
                    var constcructors = type.GetConstructors();
                    foreach (var constructor in constcructors)
                    {
                        var constructorParameters = constructor.GetParameters();

                        var parametersForInitialization = new List<object>();

                        foreach (var constructorParameter in constructorParameters)
                        {
                            if (!addedTypes.ContainsKey(constructorParameter.ParameterType.FullName))
                                throw new Exception($"{constructorParameter.ParameterType.FullName} is not added to container " +
                                    $"which is required to create instance of {type.FullName} through a constructor");

                            parametersForInitialization.Add(addedTypes[constructorParameter.ParameterType.FullName]);
                        }

                        return Activator.CreateInstance(type,
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                            null,
                            parametersForInitialization.ToArray(),
                            null);
                    }
                }

                foreach (var customAttribute in type.CustomAttributes)
                {
                    if (!addedTypes.ContainsKey(customAttribute.AttributeType.FullName))
                        throw new Exception($"{customAttribute.AttributeType.FullName} is not added to container " +
                            $"which is required to create instance of {type.FullName}");
                }
            }

            if (type.GetProperties().Any())
            {
                foreach (var property in type.GetProperties())
                {
                    if (!addedTypes.ContainsKey(property.PropertyType.FullName))
                        throw new Exception($"{property.PropertyType.FullName} is not added to container " +
                            $"which is required to create instance of {type.FullName}");
                }
            }

            return Activator.CreateInstance(type);
        }

        public T CreateInstance<T>()
        {
            return (T)this.CreateInstance(typeof(T));
        }
    }
}
