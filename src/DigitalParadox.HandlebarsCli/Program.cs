using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using CommandLine;
using CommandLine.Text;
using DigitalParadox.HandlebarsCli;
using Microsoft.Practices.Unity;

namespace DigitalParadox.ScriptRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ConfigurationTools.LoadAppConfig();


            using (var bootstrapper = new Bootstrapper(config))
            {
                bootstrapper.Setup();

                var app = bootstrapper.Resolve<Application>();
                app.Run();
            }

        }

    }
}


