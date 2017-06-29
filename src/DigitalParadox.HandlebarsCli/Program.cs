using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CommandLine;
using DigitalParadox.HandlebarsCli.Utilities;
using DigitalParadox.HandlebarsCli.Verbs;
using Microsoft.Practices.Unity;

namespace DigitalParadox.HandlebarsCli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var bootstrapper = new Bootstrapper())
            {

                bootstrapper.Setup();
                var verb = bootstrapper.Resolve<IVerbDefinition>();
                verb.Execute();

            }
        }
    }

    public class UnityParser : Parser
    {
        private readonly IUnityContainer _container;

        public UnityParser(IUnityContainer container, ParserSettings settings)
        {
            _container = container;
        }

        public new ParserResult<T> ParseArguments<T>(IEnumerable<string> args) where T : new()
        {
            return this.ParseArguments(() => _container.Resolve<T>(), args );
        }
    }
}