using System.IO;
using HandlebarsCli.Plugins;
using DigitalParadox.HandlebarsCli.Plugins;

namespace HandlebarsCli.Plugins
{
    public interface IHandlebarsHelper : IPlugin
    {
        string Name { get; set; }
        HelperType Type { get; set; }
        string Execute(TextWriter writer, HelperOptions options, dynamic data, params object[] args);
    }
}