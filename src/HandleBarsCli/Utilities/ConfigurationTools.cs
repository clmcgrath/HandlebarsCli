using System.IO;
using Configuration = HandlebarsCli.Models.Configuration;
using DigitalParadox.Parsers.Yaml;
using HandlebarsCli.Utilities;
using HandlebarsCli.Interfaces;

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