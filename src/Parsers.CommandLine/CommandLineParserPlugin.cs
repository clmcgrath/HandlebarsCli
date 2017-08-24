using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using CommandLine;
using DigitalParadox.Parsers.CommandLine;
using DigitalParadox.Utilities.AssemblyLoader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Parsers.CommandLine.ObjectFactory;

namespace Parsers.CommandLine
{
    
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandLineParser(this IServiceCollection services, IServiceProvider provider)
        {
            var verbs = AssemblyLoader.GetAssemblies<IVerbDefinition>(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory))
                .GetTypes<IVerbDefinition>().Where(q => !q.IsInterface);

            verbs.ForEach(q =>
            {
                services.AddTransient(typeof(IVerbDefinition),q);
            });


            services.AddTransient<IEnumerable<IVerbDefinition>>(inject=> inject.GetServices<IVerbDefinition>());

            services.AddTransient<IVerbResolver, VerbResolver>();

            services.AddTransient<IVerbDefinition>(inject =>
            {
                var resolver = inject.GetService<IVerbResolver>();
                return resolver.Resolve(Environment.GetCommandLineArgs());
            });

            services.AddTransient(inject => new Parser(settings => inject.GetService<ParserSettings>()));
            
            ParserSettings.ObjectFactory = new MicrosoftDIAbstractionObjectFactory(provider);

            services.AddTransient<ParserSettings>( inject => 
                new ParserSettings()
                {
                    CaseInsensitiveEnumValues = true,
                    EnableDashDash = true,
                    CaseSensitive = false
                });
            return services;

        }
    }

    public  class CommandLineParserPlugin : UnityContainerExtension
    {
        protected override void Initialize()
        {

            var verbs = AssemblyLoader.GetAssemblies<IVerbDefinition>(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory))
                .GetTypes<IVerbDefinition>().Where(q => !q.IsInterface);

            verbs.ForEach(q =>
            {
                Container.RegisterType(typeof(IVerbDefinition), q, q.AssemblyQualifiedName, new ExternallyControlledLifetimeManager());
            });

            Container.RegisterType<IEnumerable<IVerbDefinition>>(
                new InjectionFactory(inject => Container.ResolveAll<IVerbDefinition>()));

            Container.RegisterInstance(new Parser(settings => Container.Resolve<ParserSettings>()));

            ParserSettings.ObjectFactory = new UnityObjectFactory(Container);

            Container.RegisterType<IVerbResolver, VerbResolver>();

            Container.RegisterType<IVerbDefinition>(new InjectionFactory(inject =>
            {
                var resolver = inject.Resolve<IVerbResolver>();
                return resolver.Resolve(Environment.GetCommandLineArgs());
            }));


            Container.RegisterInstance(new Parser(settings => Container.Resolve<ParserSettings>()));
            
            ParserSettings.ObjectFactory  = new UnityObjectFactory(Container);

            Container.RegisterInstance(
                new ParserSettings()
                {
                    CaseInsensitiveEnumValues = true,
                    EnableDashDash = true,
                    CaseSensitive = false
                });
        }

    }
}
