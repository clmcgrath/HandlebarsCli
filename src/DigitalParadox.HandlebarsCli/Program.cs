using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DigitalParadox.HandlebarsCli.Interfaces;
using DigitalParadox.HandlebarsCli.Utilities;
using DigitalParadox.HandlebarsCli.Verbs;
using DigitalParadox.Parsing.CommandLine;
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
                Console.WriteLine("Brutalize a key with your favourite finger to exit.");
                Console.ReadKey();
            }
        }
    }
}