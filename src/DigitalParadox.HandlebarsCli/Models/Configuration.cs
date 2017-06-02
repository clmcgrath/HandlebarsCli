using System.Collections.Generic;

namespace DigitalParadox.HandlebarsCli.Models
{
    public class Configuration
    {
        public Configuration()
        {
            PluginDirectories = new List<string>();
        }

        public ICollection<string> PluginDirectories { get; set; }
    }
}