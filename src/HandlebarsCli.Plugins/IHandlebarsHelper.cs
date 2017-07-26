using System.IO;
using DigitalParadox.Plugins;
using HandlebarsCli.Plugins.Helpers;
using HandlebarsDotNet;

namespace HandlebarsCli.Plugins
{
    public interface IHandlebarsHelper : IPlugin
    {
        string Name { get; set; }
        HelperType Type { get; set; }
        string Execute(TextWriter writer, HelperOptions options, dynamic data, params object[] args);
    }
}