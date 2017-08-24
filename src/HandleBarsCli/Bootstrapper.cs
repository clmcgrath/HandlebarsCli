using System.ComponentModel;
using DigitalParadox.Logging.Serilog;
using DigitalParadox.Parsers.Yaml;
using HandlebarsCli.HandlebarsTemplateProcessor;
using HandleBarsCLI.Config;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace HandleBarsCLI
{
    public class Bootstrapper : UnityContainer
    {
        public virtual void Setup()
        {
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(this));

            //external container setup 
            this.AddNewExtension<SerilogPlugin>()
                .AddNewExtension<HandlebarsTemplateProcessorExtension>()
                .AddNewExtension<YamlUnityExtension>()
                //.AddNewExtension<CommandLineParserPlugin>()
                .AddNewExtension<ConfigurationSetup>()
                .AddNewExtension<PluginConfiguration>();
            
        }
    }
}