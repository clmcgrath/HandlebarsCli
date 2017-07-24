using System.IO;
using HandlebarsDotNet;

namespace DigitalParadox.HandlebarsCli.Plugins.Helpers
{
    public class HelloWorldExamplePlugin : IHandlebarsHelper
    {
        public string Name { get; set; } = "HelloWorld";
        public HelperType Type { get; set; } = HelperType.Inline;

        public string Execute(TextWriter writer, HelperOptions options, dynamic data, params object[] args)
        {
            return "Hello World";
        }
    }
}