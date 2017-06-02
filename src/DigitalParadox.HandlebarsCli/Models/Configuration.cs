using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalParadox.HandlebarsCli.Plugins;

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
