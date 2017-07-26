using System.IO;
using DigitalParadox.Parsers.Serializers;
using Configuration = HandleBarsCLI.Models.Configuration;

namespace HandleBarsCLI.Utilities
{
    public class ConfigurationTools
    {
        private readonly IYamlParser _parser;


        public ConfigurationTools(IYamlParser parser)
        {
            _parser = parser;
        }

        public Configuration LoadAppConfig()
        {
            

            var yaml = File.ReadAllText(Path.Combine(typeof(Program).Assembly.ToDirectoryPath(),  "config.yaml"));
     
            var parsed = _parser.Deserialize<Configuration>(yaml);

            return parsed;
        }



    }





}