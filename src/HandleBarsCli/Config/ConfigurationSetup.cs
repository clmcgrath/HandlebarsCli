using System.Collections.Generic;
using DigitalParadox.Plugins.Loader;
using HandleBarsCLI.Models;
using HandleBarsCLI.Utilities;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using HandlebarsCli.Plugins;

namespace HandleBarsCLI.Config
{
    public class PluginConfiguration : UnityContainerExtension
    {
        protected override void Initialize()
        {

            var helpers = PluginsLoader.GetPlugins<IHandlebarsHelper>();

            helpers.ForEach(q =>
            {
                Container.RegisterType(typeof(IHandlebarsHelper), q.Value, q.Key);
            });

            Container.RegisterType<ICollection<IHandlebarsHelper>>(
                new InjectionFactory(inject => Container.ResolveAll<IHandlebarsHelper>()));
            
        }
    }

    public class ConfigurationSetup : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<Configuration>();
            Container.RegisterType<ConfigurationTools>();
            Container.RegisterInstance(Container.Resolve<ConfigurationTools>().LoadAppConfig());
            Container.RegisterInstance(Container.Resolve<Configuration>()?.ProcessorOptions);
        }
    }
}
