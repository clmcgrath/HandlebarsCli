using System.Collections.Generic;
using DigitalParadox.Providers.Interfaces;

namespace DigitalParadox.Providers
{
    public static class MetaDataExtensions
    {
        public static IExecutionResult Execute(this IActionProvider provider, Dictionary<string, string> args, string displayName)
        {
            return provider.Execute(args);
        }
    }
}