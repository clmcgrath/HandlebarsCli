using System.Collections.Generic;
using DigitalParadox.HandlebarsCli.Models;
using DigitalParadox.HandlebarsCli.Utilities;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using DigitalParadox.HandlebarsCli.Plugins;

namespace DigitalParadox.HandlebarsCli.Config
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
