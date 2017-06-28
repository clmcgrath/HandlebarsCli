using System;
using System.Collections.Generic;
using System.Linq;
using DigitalParadox.HandlebarsCli.Models;
using DigitalParadox.HandlebarsCli.Plugins;
using DigitalParadox.HandlebarsCli.Utilities;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace DigitalParadox.HandlebarsCli
{
    public class Bootstrapper : UnityContainer
    {
        public virtual void Setup()
        {
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(this));
            var helpers = PluginsLoader.GetPlugins<IHandlebarsHelper>();

            helpers.ForEach(q =>
            {
                RegisterType(typeof(IHandlebarsHelper), q.Value, q.Key, new ContainerControlledLifetimeManager(),
                    new InjectionMember[0]);
            });

            this.RegisterType<ICollection<IHandlebarsHelper>>(
                new InjectionFactory(inject => this.ResolveAll<IHandlebarsHelper>()));

            var verbs = AssemblyLoader.GetAssemblies<IVerbDefinition>()
                                           .GetTypes<IVerbDefinition>().Where(q=>!q.IsInterface);
    
            verbs.ForEach(q =>
            {
                RegisterType(typeof(IVerbDefinition),q ,q.AssemblyQualifiedName, new ContainerControlledLifetimeManager(),
                    new InjectionMember[0]);
            });

            this.RegisterInstance(ConfigurationTools.LoadAppConfig());

            //this.RegisterInstance(Options.Parse(Environment.GetCommandLineArgs().Skip(1)));
            
            this.RegisterType<IVerbResolver, VerbResolver>();

            this.RegisterType<IVerbDefinition>(new InjectionFactory(inject =>
            {
                var resolver = inject.Resolve<IVerbResolver>();
                return resolver.Resolve(Environment.GetCommandLineArgs());
            }));

            //Registrations.Where(q => typeof(IProvider).IsAssignableFrom(q.MappedToType))
            //    .ForEach(q => Console.WriteLine($"Registered Plugin {q.Name} ({q.MappedToType.FullName})"));
        }
    }
}