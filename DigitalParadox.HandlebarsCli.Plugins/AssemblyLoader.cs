using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DigitalParadox.Providers.Actions;
using DigitalParadox.Providers.Interfaces;

namespace DigitalParadox.HandlebarsCli.Plugins
{
    public static class AssemblyLoader
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
        public static IEnumerable<Type> GetTypes<T>(this Assembly assembly)
        {
            return GetTypes<T>(new List<Assembly> { assembly });
        }

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

    public static class Providers
    {

        internal static IEnumerable<Type> GetProviderCollection<T>(this IEnumerable<Assembly> assemblies)
            where T : IProvider
        {
            return assemblies.SelectMany(a => a.GetProviderCollection<T>().Where(q=>!q.IsInterface));
        }

        internal static IEnumerable<Type> GetProviderCollection<T>(this Assembly assembly)
            where T : IProvider
        {
            return AssemblyLoader.GetTypes<T>();
        }

        public static IDictionary<string, Type> GetProviders<T>()
            where T : IProvider
        {
            var types = AssemblyLoader.GetAssemblies<T>().GetProviderCollection<T>();

            var dictionary = new Dictionary<string, Type>();

            foreach (var type in types)
            {
                var pInfo = type.GetCustomAttribute(typeof(ProviderInfoAttribute)) as ProviderInfoAttribute;
                if (!string.IsNullOrWhiteSpace(pInfo?.Name))
                {
                    dictionary.Add(pInfo.Name, type);
                }

                dictionary.Add(type.FullName, type);
            }
            return dictionary;
        }

        public static IDictionary<string, Type> GetProviders<T>(this Assembly assembly)
            where T:IProvider
        {
            Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
            foreach (var type in assembly.GetProviderCollection<T>())
            {

                ProviderInfoAttribute pInfo = type.GetCustomAttribute(typeof(ProviderInfoAttribute)) as ProviderInfoAttribute;
                if (!string.IsNullOrWhiteSpace(pInfo?.Name))
                {
                    dictionary.Add(pInfo.Name, type);
                }

                dictionary.Add(type.FullName, type);
            }
            return dictionary;
        }

        public static IDictionary<string, Type> GetProviders<T>(this IEnumerable<Assembly> assemblies)
            where T : IProvider
        {
            
            return null;
        }
    }

}