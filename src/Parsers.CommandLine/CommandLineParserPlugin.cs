using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using HandlebarsCli.Interfaces;
using DigitalParadox.Utilities.AssemblyLoader;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace DigitalParadox.Parsing.CommandLineParser
{
    public  class CommandLineParserPlugin : UnityContainerExtension
    {
        protected override void Initialize()
        {

            var verbs = AssemblyLoader.GetAssemblies<IVerbDefinition>()
                .GetTypes<IVerbDefinition>().Where(q => !q.IsInterface);

            verbs.ForEach(q =>
            {
                Container.RegisterType(typeof(IVerbDefinition), q, q.AssemblyQualifiedName, new ExternallyControlledLifetimeManager());
            });

            Container.RegisterType<IEnumerable<IVerbDefinition>>(
                new InjectionFactory(inject => Container.ResolveAll<IVerbDefinition>()));


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
