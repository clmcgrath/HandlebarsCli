using System;
using System.Diagnostics;
using System.Linq;
using DigitalParadox.HandlebarsCli.Models;
using DigitalParadox.HandlebarsCli.Plugins;
using DigitalParadox.Providers;
using DigitalParadox.Providers.Interfaces;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;


namespace DigitalParadox.ScriptRunner
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

            Console.WindowWidth = 120;

            var actionProviders = HandlebarsCli.Plugins.Providers.GetProviders<IHandlebarsPlugin>();
            //var configProviders = Providers.Providers.GetProviders<IConfigurationProvider>();

            actionProviders.ForEach(q =>
            {

                this.RegisterType(typeof(IHandlebarsPlugin), q.Value, q.Key, new ContainerControlledLifetimeManager(),
                    new InjectionMember[0]);
            });




            this.Registrations.Where(q=>typeof(IProvider).IsAssignableFrom(q.MappedToType)).ForEach(q=> Console.WriteLine($"Registered Plugin { q.Name } ({ q.MappedToType.FullName })"));
        }



    }
}