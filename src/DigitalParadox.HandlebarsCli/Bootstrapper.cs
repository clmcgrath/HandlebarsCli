using DigitalParadox.HandlebarsCli.Config;
using DigitalParadox.HandlebarsCli.Services.HandlebarsTemplateProcessor;
using DigitalParadox.Logging;
using DigitalParadox.Parsers.Yaml;
using DigitalParadox.Parsing.CommandLine;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace DigitalParadox.HandlebarsCli
{
    public class Bootstrapper : UnityContainer
    {
        public virtual void Setup()
        {
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(this));

            //external container setup 
            this.AddNewExtension<SerilogPlugin>()
                .AddNewExtension<HandlebarsTemplateProcessorExtension>()
                .AddNewExtension<YamlUnityExtension>();

            //internal container setup
            this.AddNewExtension<CommandLineParserPlugin>()
                .AddNewExtension<ConfigurationSetup>()
                .AddNewExtension<PluginConfiguration>();

        }
        
    }
}