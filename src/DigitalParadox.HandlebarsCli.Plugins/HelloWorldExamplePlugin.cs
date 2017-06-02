using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandlebarsDotNet;

namespace DigitalParadox.HandlebarsCli.Plugins
{
    [PluginInfo(Name = "HelloWorld", Description = "Outputs Hello World Text in a Handlebars template ", DisplayName = "Hello World", PluginType = PluginType.Inline)]
    public class HelloWorldExamplePlugin : IHandlebarsHelper
    {
        public string Name { get; set; } = "HelloWorld";
        public PluginType PluginType { get; set; } = PluginType.Inline;


        public string Execute(TextWriter writer, HelperOptions options, dynamic data, params object[] args)
        {
            return "Hello World";
        }
    }
}
