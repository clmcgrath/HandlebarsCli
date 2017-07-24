using System.IO;
using DigitalParadox.HandlebarsCli.Plugins.Helpers;
using HandlebarsDotNet;

namespace DigitalParadox.HandlebarsCli.Plugins
{
    public interface IHandlebarsHelper : IProvider
    {
        string Name { get; set; }
        HelperType Type { get; set; }
        string Execute(TextWriter writer, HelperOptions options, dynamic data, params object[] args);
    }
}