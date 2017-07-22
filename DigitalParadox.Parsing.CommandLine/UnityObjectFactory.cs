using System;
using CommandLine.Infrastructure;
using Microsoft.Practices.Unity;

namespace DigitalParadox.Parsing.CommandLineParser
{

    public class UnityObjectFactory : IObjectFactory
    {
        public UnityObjectFactory(IUnityContainer container)
        {
            _container = container;
        }

        private readonly IUnityContainer _container;
        public T Resolve<T>() => _container.Resolve<T>();
        public object Resolve(Type type) => _container.Resolve(type);
    }

}
