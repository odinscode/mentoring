using System;
using System.Reflection;

namespace CustomReflectionLibrary.Services
{
    public class Container
    {
        public void AddAssemlby(Assembly assembly)
        {

        }

        public void AddType(Type type)
        {

        }

        public void AddType(Type type, Type baseType)
        {

        }

        public object CreateInstance(Type type)
        {
            return null;
        }

        public T CreateInstance<T>()
        {
            return default(T);
        }
    }
}
