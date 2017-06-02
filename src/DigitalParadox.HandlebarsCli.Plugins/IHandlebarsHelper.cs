using System.IO;
using HandlebarsDotNet;

namespace DigitalParadox.HandlebarsCli.Plugins
{
    public interface IHandlebarsHelper : IProvider
    {
        string Execute(TextWriter writer, HelperOptions options,  dynamic data, params object[] args);
        string Name { get; set; } 
    }
}