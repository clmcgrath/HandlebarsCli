using System.IO;
using DigitalParadox.HandlebarsCli.Interfaces;
using DigitalParadox.Parsers.Yaml;

using Configuration = DigitalParadox.HandlebarsCli.Models.Configuration;

namespace DigitalParadox.HandlebarsCli.Utilities
{
    public class ConfigurationTools
    {
        private readonly IYamlParser _parser;
        private readonly ITemplateProcessorOptions _options;

        public ConfigurationTools(IYamlParser parser, ITemplateProcessorOptions options)
        {
            _parser = parser;
            _options = options;
        }

        public Configuration LoadAppConfig()
        {
            

            var yaml = File.ReadAllText(Path.Combine(typeof(Program).Assembly.ToDirectoryPath(),  "config.yaml"));
     
            var parsed = _parser.Deserialize<Configuration>(yaml);

            return parsed;
        }



    }





}