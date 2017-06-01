using System;
using System.Collections.Generic;
using System.IO;
using DigitalParadox.HandlebarsCli.Models;
using DigitalParadox.HandlebarsCli.Plugins;
using HandlebarsDotNet;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;

namespace DigitalParadox.HandlebarsCli
{
    public class Application
    {
        public Options Options { get; }
        public ICollection<IHandlebarsPlugin> Plugins { get; }


        public Application(Options options, List<IHandlebarsPlugin> plugins )
        {
            Options = options;
            Plugins = plugins;
        }

        public void Run()
        {
                        Plugins.ForEach(p => Handlebars.RegisterHelper(
                            p.Name,
                            (writer, options, context, arguments) => p.Execute(writer, options, context, arguments)));

                        var psComment = File.ReadAllText(Options.Template);
            
                        var template = Handlebars.Compile(psComment);
            
                        object model;
            
                        if (File.Exists(Options.Data))
                        {
                            //WriteVerbose("");
                            File.ReadAllText(Options.Data);
            
                            model = JsonConvert.DeserializeObject(Options.Data);
                        }
                        else
                        {
                            model = JsonConvert.DeserializeObject(Options.Data);
                        }
            
            
                        var data = Options.Data;
                        
            
                        var result = template(data);
            
                        Console.WriteLine(result);
        }
    }
}