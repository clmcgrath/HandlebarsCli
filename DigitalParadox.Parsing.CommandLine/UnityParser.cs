using CommandLine;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace DigitalParadox.HandlebarsCli
{
    public class UnityParser : Parser
    {
        private readonly IUnityContainer _container;

        public UnityParser(IUnityContainer container, ParserSettings settings)
        {
            _container = container;
        }
        
        public new ParserResult<T> ParseArguments<T>(IEnumerable<string> args) where T : new()
        {
            return base.ParseArguments(() => _container.Resolve<T>(), args);
        }
    }
}