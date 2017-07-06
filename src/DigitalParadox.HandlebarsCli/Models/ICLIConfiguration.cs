using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using DigitalParadox.HandlebarsCli.Interfaces;
using DigitalParadox.HandlebarsCli.Services.HandlebarsTemplateProcessor;
using Newtonsoft.Json;

namespace DigitalParadox.HandlebarsCli.Models
{
    public interface ICLIConfiguration
    {
        
        ICollection<string> PluginDirectories { get; set; }
        ITemplateProcessorOptions ProcessorOptions { get; set; }
    }

    public class Configuration : ICLIConfiguration
    {
        public Configuration(HandlebarsProcessorOptions processorOptions)
        {
            if (processorOptions != null)
            {
                ProcessorOptions = processorOptions;
            }
            else
            {
                ProcessorOptions = new HandlebarsProcessorOptions(new ViewOptions());
            }
        }

        public ICollection<string> PluginDirectories { get; set; }
        public ITemplateProcessorOptions ProcessorOptions { get; set; }
    }
}