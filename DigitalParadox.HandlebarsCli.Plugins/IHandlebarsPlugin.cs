using DigitalParadox.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalParadox.Providers.Interfaces;
using HandlebarsDotNet;

namespace DigitalParadox.HandlebarsCli.Plugins
{
    
    public interface IHandlebarsPlugin : IProvider
    {
        string Name { get; set; }
        string Execute(dynamic data);
        string Execute(TextWriter writer, dynamic data, params object[] args);
        string Execute(TextWriter writer, HelperOptions options,  dynamic data, params object[] args);

    }

    public interface IHandlebarsBlockPlugin : IProvider
    {
        string Name { get; set; }
        string Execute(dynamic data);
        Hand Execute();
    }


}

namespace DigitalParadox.Providers.Interfaces
{
    public interface IProvider
    {
    }
}

