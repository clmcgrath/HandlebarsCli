using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using DigitalParadox.HandlebarsCli.Interfaces;
using DigitalParadox.HandlebarsCli.Models;
using DigitalParadox.HandlebarsCli.Plugins;
using DigitalParadox.HandlebarsCli.Services.HandlebarsTemplateProcessor;
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


            this.AddNewExtension<HandlebarsTemplateProcessorExtension>();

            var helpers = PluginsLoader.GetPlugins<IHandlebarsHelper>();

            helpers.ForEach(q =>
            {
                RegisterType(typeof(IHandlebarsHelper), q.Value, q.Key, new TransientLifetimeManager(), 
                    new InjectionMember[0]);
            });



            this.RegisterInstance(this.Resolve<Configuration>().ProcessorOptions);

            var verbs = AssemblyLoader.GetAssemblies<IVerbDefinition>()
                                           .GetTypes<IVerbDefinition>().Where(q=>!q.IsInterface);
    
            verbs.ForEach(q =>
            {
                RegisterType(typeof(IVerbDefinition),q ,q.AssemblyQualifiedName, new TransientLifetimeManager(), 
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

            this.RegisterType<Parser, UnityParser>();
            this.RegisterInstance(
                new ParserSettings()
                {
                    CaseInsensitiveEnumValues = true,
                    EnableDashDash = true,
                    CaseSensitive = false
                });

            this.RegisterType<ICollection<IHandlebarsHelper>>(
                new InjectionFactory(inject => this.ResolveAll<IHandlebarsHelper>()));

            this.RegisterType<ICollection<IVerbDefinition>>(
                new InjectionFactory(inject => this.ResolveAll<IVerbDefinition>()));
            //if (!this.Resolve<IVerbResolver>().Resolve().Verbose) return;
            //
            //    Console.WriteLine("Registered Helpers:");
            //    Registrations.Where(q => typeof(IHandlebarsHelper).IsAssignableFrom(q.MappedToType))
            //        .ForEach(q => Console.WriteLine($"Registered Plugin {q.Name} ({q.MappedToType.FullName})"));

        }
    }
}