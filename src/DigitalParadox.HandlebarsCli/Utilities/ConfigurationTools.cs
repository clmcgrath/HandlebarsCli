using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ConfigR;
using ConfigR.Sdk;
using DigitalParadox.HandlebarsCli.Models;
using DigitalParadox.HandlebarsCli.Services.HandlebarsTemplateProcessor;
using Newtonsoft.Json;

namespace DigitalParadox.HandlebarsCli.Utilities
{
    public static class ConfigurationTools
    {
        public static Configuration LoadAppConfig()
        {
            return new Configuration(new HandlebarsProcessorOptions(new ViewOptions()));
        }
    }


}