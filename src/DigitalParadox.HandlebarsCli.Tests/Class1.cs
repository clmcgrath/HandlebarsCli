using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalParadox.HandlebarsCli.Models;
using DigitalParadox.HandlebarsCli.Plugins;
using Xunit;

namespace DigitalParadox.HandlebarsCli.Tests
{
    public class HandlebarsHelpersLoaderTests
    {
        [Fact]
        public void HandleBarsPluginRendersText()
        {
            
        }
        [Fact]
        public void AssemblyLoaderLoadsPluginsDynamically()
        {
            var plugins = Plugins.PluginsLoader.GetPlugins<IHandlebarsHelper>();
            Assert.NotEmpty(plugins);
        }

        [Fact]
        public void BootstrapperResolvesFromAssemblyLoader()
        {
            Bootstrapper bootstrapper = new Bootstrapper(new Configuration(){ PluginDirectories = new List<string>(){ ".\\Plugins" }});
            bootstrapper.Setup();
            var plugin = bootstrapper.Resolve(typeof(IHandlebarsHelper), "HelloWorld");

            Assert.NotNull(plugin);

            Assert.IsType<HelloWorldExamplePlugin>(plugin);
            
        }


        

    }
}
