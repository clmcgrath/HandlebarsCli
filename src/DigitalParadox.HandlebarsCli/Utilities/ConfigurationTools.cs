using System.IO;
using DigitalParadox.HandlebarsCli.Models;
using Newtonsoft.Json;

namespace DigitalParadox.ScriptRunner
{
    public static class ConfigurationTools
    {

        public static Configuration LoadAppConfig()
        {
            var json = File.ReadAllText("config.json");
            return JsonConvert.DeserializeObject<Configuration>(json);
        }
    }
}