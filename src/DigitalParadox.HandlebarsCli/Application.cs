using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Xml;
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
        public ICollection<IHandlebarsHelper> Plugins { get; }

        // Unity requires Concrete Type for collections unless you configure explicitly 
        // ReSharper disable once SuggestBaseTypeForParameter
        public Application(Options options, System.Collections.Generic.List<IHandlebarsHelper> plugins)
        {
            Options = options;
            Plugins = plugins;
        }

        public void Run()
        {
            Plugins.ForEach(p => Handlebars.RegisterHelper(
                p.Name,
                (writer, options, context, arguments) => p.Execute(writer, options, context, arguments)));

            var template = File.ReadAllText(Options.Template);
            
            var compiledTemplate = Handlebars.Compile(template);

            if (File.Exists(Options.Data))
            {
                Options.Data = File.ReadAllText(Options.Data);
            }

            var model = JsonConvert.DeserializeObject<ExpandoObject>(Options.Data);

            var result = compiledTemplate(model);
            //if output file not specified write to console 
            if (string.IsNullOrWhiteSpace(Options.OutputFile))
            {
                Console.WriteLine(result);
                return;
            }
            //otherwise output to file 
            var file = new FileInfo(Options.OutputFile);
            
            File.WriteAllText(Options.OutputFile, result, Encoding.UTF8);

            Console.WriteLine(file.FullName);
        }
    }
}