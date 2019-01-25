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
            throw new NotImplementedException();
        }

        public object CreateInstance(Type type)
        {
            if (!addedTypes.ContainsKey(type.FullName))
                throw new Exception($"{type.FullName} is not added to container");

            if (type.CustomAttributes.Any())
            {
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
