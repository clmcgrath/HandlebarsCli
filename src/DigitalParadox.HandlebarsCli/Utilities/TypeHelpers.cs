using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DigitalParadox.HandlebarsCli.Utilities
{
    public static class TypeHelpers
    {
        /// <summary>
        /// Search Assembly and return specified types the deririve from given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly">Assembly to search</param>
        /// <returns>Collection of derived types of <see cref="T"/></returns>
        public static IEnumerable<Type> FindDerivedTypes<T>(Assembly assembly)
        {
            //if (typeof(T).IsInterface)
            //{
            //    return assembly.GetTypes().Where(q => q.GetInterface(typeof(T).FullName) != null);
            //}
            return assembly.GetTypes().Where(q => typeof(T).IsAssignableFrom(q));
        }

        /// <summary>
        /// Get collection of assembly that contain types derived from <see cref="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAssemblies<T>()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var filtered = assemblies.Where(q => FindDerivedTypes<T>(q).Any());
            return filtered;
        }

        /// <summary>
        /// Find All Derived Types of <see cref="T"/>in a collection of assemblies 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypes<T>(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(FindDerivedTypes<T>);
        }


        public static IEnumerable<Type> GetTypes<T>(string nameOrFile = null)
        {

            if (nameOrFile == null)
            {
                return GetAssemblies<T>().GetTypes<T>();
            }
            var assembly = Assembly.LoadFrom(nameOrFile);

            var types = FindDerivedTypes<T>(assembly);
            return types.Where(q => !q.IsAbstract && !q.IsInterface);
        }
    }
}