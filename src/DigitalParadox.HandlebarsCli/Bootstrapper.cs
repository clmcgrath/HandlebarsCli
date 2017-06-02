using System;
using System.Collections.Generic;
using System.Linq;
using DigitalParadox.HandlebarsCli.Models;
using DigitalParadox.HandlebarsCli.Plugins;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;


namespace DigitalParadox.HandlebarsCli
{
    public class Bootstrapper : UnityContainer
    {
        private readonly Configuration _config;


        public Bootstrapper(Configuration config)
        {
            _config = config;
        }

        public virtual void Setup()
        {
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(this));
            var helpers = HandlebarsCli.Plugins.PluginsLoader.GetPlugins<IHandlebarsHelper>();
            
            helpers.ForEach(q =>
            {

                this.RegisterType(typeof(IHandlebarsHelper), q.Value, q.Key, new ContainerControlledLifetimeManager(),
                    new InjectionMember[0]);
            });
            
               

            this.Registrations.Where(q=>typeof(IProvider).IsAssignableFrom(q.MappedToType)).ForEach(q=> Console.WriteLine($"Registered Plugin { q.Name } ({ q.MappedToType.FullName })"));
        }

    }
}