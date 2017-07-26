using System;
using DigitalParadox.Parsers.CommandLine;
using Microsoft.Practices.Unity;

namespace HandleBarsCLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var bootstrapper = new Bootstrapper())
            {
                bootstrapper.Setup();
                var resolver = bootstrapper.Resolve<IVerbResolver>();
                var verb = resolver.Resolve(args);
                verb.Execute();
                Console.WriteLine("\n\nBrutalize a key with your favourite finger to exit.");
                Console.ReadKey();
            }
        }
    }
}