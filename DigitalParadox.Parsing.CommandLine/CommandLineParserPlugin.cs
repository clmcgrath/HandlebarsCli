using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using DigitalParadox.HandlebarsCli;
using DigitalParadox.HandlebarsCli.Interfaces;
using DigitalParadox.HandlebarsCli.Plugins;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace DigitalParadox.Parsing.CommandLine
{
    public class CommandLineParserPlugin : UnityContainerExtension
    {
        protected override void Initialize()
        {

            var verbs = AssemblyLoader.GetAssemblies<IVerbDefinition>()
                .GetTypes<IVerbDefinition>().Where(q => !q.IsInterface);

            verbs.ForEach(q =>
            {
                Container.RegisterType(typeof(IVerbDefinition), q, q.AssemblyQualifiedName, new ExternallyControlledLifetimeManager());
            });

            Container.RegisterType<IVerbDefinition>(new InjectionFactory(inject =>
            {
                var resolver = inject.Resolve<IVerbResolver>();
                return resolver.Resolve(Environment.GetCommandLineArgs());
            }));

            Container.RegisterType<IVerbResolver, VerbResolver>();



            Container.RegisterType<Parser, UnityParser>();

            Container.RegisterInstance(
                new ParserSettings()
                {
                    CaseInsensitiveEnumValues = true,
                    EnableDashDash = true,
                    CaseSensitive = false
                });

            Container.RegisterType<IEnumerable<IVerbDefinition>>(
                new InjectionFactory(inject => Container.ResolveAll<IVerbDefinition>()));
        }
    }
}
