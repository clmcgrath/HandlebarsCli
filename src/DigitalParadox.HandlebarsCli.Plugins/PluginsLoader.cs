using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DigitalParadox.HandlebarsCli.Plugins.Helpers;

namespace DigitalParadox.HandlebarsCli.Plugins
{
    public static class PluginsLoader
    {
        internal static IEnumerable<Type> GetPluginCollection<T>(this IEnumerable<Assembly> assemblies)
            where T : IProvider
        {
            return assemblies.SelectMany(a => GetPluginCollection<T>(a).Where(q => !q.IsInterface));
        }

        internal static IEnumerable<Type> GetPluginCollection<T>(this Assembly assembly)
            where T : IProvider
        {
            return AssemblyLoader.GetTypes<T>();
        }

        public static IDictionary<string, Type> GetPlugins<T>()
            where T : IProvider
        {
            var types = AssemblyLoader.GetAssemblies<T>().GetPluginCollection<T>();

            var dictionary = new Dictionary<string, Type>();

            foreach (var type in types)
            {
                var pInfo = type.GetCustomAttribute(typeof(PluginInfoAttribute)) as PluginInfoAttribute;
                if (!string.IsNullOrWhiteSpace(pInfo?.Name))
                    dictionary.Add(pInfo.Name, type);

                dictionary.Add(type.FullName, type);
            }
            return dictionary;
        }

        public static IDictionary<string, Type> GetPlugins<T>(this Assembly assembly)
            where T : IProvider
        {
            var dictionary = new Dictionary<string, Type>();
            foreach (var type in assembly.GetPluginCollection<T>())
            {
                var pInfo = type.GetCustomAttribute(typeof(PluginInfoAttribute)) as PluginInfoAttribute;
                if (!string.IsNullOrWhiteSpace(pInfo?.Name))
                    dictionary.Add(pInfo.Name, type);

                dictionary.Add(type.FullName, type);
            }
            return dictionary;
        }

        public static IDictionary<string, Type> GetPlugins<T>(this IEnumerable<Assembly> assemblies)
            where T : IProvider
        {
            return null;
        }
    }
}