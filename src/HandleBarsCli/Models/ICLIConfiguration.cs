using System.Collections.Generic;
using DigitalParadox.HandlebarsCli.Interfaces;
using Microsoft.Practices.Unity;

namespace DigitalParadox.HandlebarsCli.Models
{
    public interface ICLIConfiguration
    {
        
        ICollection<string> PluginDirectories { get; set; }
        ITemplateProcessorOptions ProcessorOptions { get; set; }
    }

    public class Configuration : ICLIConfiguration
    {
        public Configuration(ITemplateProcessorOptions processorOptions)
        {
            ProcessorOptions = processorOptions;
        }

        public ICollection<string> PluginDirectories { get; set; }
      
        public ITemplateProcessorOptions ProcessorOptions { get; set; }
    }
}