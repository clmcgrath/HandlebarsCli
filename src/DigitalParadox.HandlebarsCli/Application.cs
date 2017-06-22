using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using DigitalParadox.HandlebarsCli.Models;
using DigitalParadox.HandlebarsCli.Plugins;
using HandlebarsDotNet;
using Microsoft.Practices.ObjectBuilder2;
using Mustache;
using Newtonsoft.Json;

namespace DigitalParadox.HandlebarsCli
{
    public class Application
    {
        // Unity requires Concrete Type for collections unless you configure explicitly 
        // ReSharper disable once SuggestBaseTypeForParameter
        public Application(Options options, ICollection<IHandlebarsHelper> plugins)
        {
            Options = options;
            Plugins = plugins;
        }

        public Options Options { get; }
        public ICollection<IHandlebarsHelper> Plugins { get; }


        public void Run()
        {
            var viewsDir = Options.ViewsDirectory.Replace("{{TemplateDirectory}}", Options.TemplateDirectory);
            var templates = System.IO.Directory.GetFiles(viewsDir, "*.*", SearchOption.AllDirectories).Select(tpl => new FileInfo(tpl));
            
            templates.ForEach(tpl=> Handlebars.RegisterTemplate( tpl.Name.TrimEnd(tpl.Extension.ToCharArray()), File.ReadAllText(tpl.FullName)));

            Plugins.ForEach(p => Handlebars.RegisterHelper(
                p.Name,
                (writer, options, context, arguments) => p.Execute(writer, options, context, arguments)));
            var path = new FileInfo(Path.Combine(Options.TemplateDirectory, Options.TemplateName));
            var template = File.ReadAllText(path.FullName ,Encoding.UTF8);

            var compiledTemplate = Handlebars.Compile(template);

            if (!string.IsNullOrWhiteSpace(Options.InputFile))
            {
                var datafile = new FileInfo(Options.InputFile);
                if (!datafile.Exists)
                {
                    throw new FileLoadException(
                        "The specified file could not be loaded.", 
                        new FileNotFoundException($"File {datafile.FullName} was not found.")
                    );
                }

                Options.Data = File.ReadAllText(datafile.FullName);
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