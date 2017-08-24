using System;
using CommandLine.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Parsers.CommandLine
{
    public class MicrosoftDIAbstractionObjectFactory : IObjectFactory
    {
        public MicrosoftDIAbstractionObjectFactory(IServiceProvider container)
        {
            _container = container;
        }

        private readonly IServiceProvider _container;
        public T Resolve<T>() => ServiceProviderServiceExtensions.GetService<T>(_container);
        public object Resolve(Type type) => _container.GetService(type);
    }
}