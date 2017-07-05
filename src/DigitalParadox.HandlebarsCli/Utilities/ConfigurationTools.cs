using System;
using System.IO;
using System.Reflection;
using DigitalParadox.HandlebarsCli.Models;
using Newtonsoft.Json;

namespace DigitalParadox.HandlebarsCli.Utilities
{
    public static class ConfigurationTools
    {
        public static Configuration LoadAppConfig()
        {
            var json = File.ReadAllText(Path.Combine(typeof(Configuration).Assembly.ToDirectoryPath(),"config.json"));
            return JsonConvert.DeserializeObject<Configuration>(json);
        }

    }


}