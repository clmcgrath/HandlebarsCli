using DigitalParadox.Plugins;
using HandlebarsCli.Plugins;
using Xunit;

namespace HandlebarsCli.Tests
{
    public class HandlebarsHelpersLoaderTests
    {
        [Fact]
        public void AssemblyLoaderLoadsPluginsDynamically()
        {
            var plugins = PluginsLoader.GetPlugins<IHandlebarsHelper>();
            Assert.NotEmpty(plugins);
        }

        //[Fact]
        //public void BootstrapperResolvesFromAssemblyLoader()
        //{
        //    var bootstrapper = new Bootstrapper();
        //    bootstrapper.Setup();
        //    var plugin = bootstrapper.Resolve(typeof(IHandlebarsHelper), "HelloWorld");

        //    Assert.NotNull(plugin);

        //    Assert.IsType<HelloWorldExamplePlugin>(plugin);
        //}

        [Fact]
        public void HandleBarsPluginRendersText()
        {
        }
    }
}