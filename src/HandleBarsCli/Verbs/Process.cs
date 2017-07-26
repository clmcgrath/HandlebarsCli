using CommandLine;
using DigitalParadox.Parsers.CommandLine;
using DigitalParadox.Parsers.TemplateProcessor;
using HandleBarsCLI.Models;
using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.IO;
using System.Text;

namespace HandleBarsCLI.Verbs
{
    [Verb("process", HelpText = "Process handlebars Template")]
    public class Process : IVerbDefinition
    {
        private readonly ITemplateProcessor _processor;



        public Process(ITemplateProcessor processor, Configuration configuration)
        {
            Configuration = configuration;
            _processor = processor;
        }

        public int Execute()
        {

            var path = new FileInfo(Path.Combine(TemplateDirectory, TemplateName));
            var template = File.ReadAllText(path.FullName, Encoding.UTF8);

            if (!string.IsNullOrWhiteSpace(InputFile))
            {
                var datafile = new FileInfo(InputFile);
                if (!datafile.Exists)
                {
                    throw new FileLoadException(
                        "The specified file could not be loaded.",
                        new FileNotFoundException($"File {datafile.FullName} was not found.")
                    );
                }

                Data = File.ReadAllText(datafile.FullName);
            }

            var model = JsonConvert.DeserializeObject<ExpandoObject>(Data);


            ITemplateResult result = _processor.Process(template, model);

            if (result.HasErrors)
            {
                result.Errors.ForEach(q => Console.Error.WriteLine(q.Description));
                return 5;
            }

            //if output file not specified write to console 

            if (string.IsNullOrWhiteSpace(OutputFile))
            {
                Console.WriteLine(result.Result);
                return 0;
            }

            //otherwise output to file 
            var file = new FileInfo(OutputFile);

            File.WriteAllText(OutputFile, result.Result, Encoding.UTF8);

            Console.WriteLine(file.FullName);
            return 0;
        }

        public Configuration Configuration { get; set; }

        public string Data { get; set; }

        [Option('t', "template", SetName = "textTemplate", Required = true)]
        public string Exception { get; set; }

        [Option('i', "input", HelpText = "Input data file, supports json only at this time")]
        public string InputFile { get; set; }

        public string OutputFile { get; set; }
        [Option('b', "basedir", Default = @".\", MetaValue = "Path", HelpText = "Base directory to load templates from")]
        public string TemplateDirectory { get; set; }

        [Option('n', "template-name", Required = true, SetName = "FileTemplate", MetaValue = "TEMPLATE NAME", HelpText = "Template Name to use for output, ie pscomment.template or pscomment.hbs")]
        public string TemplateName { get; set; }

        public bool Verbose { get; set; }

        public string ViewsDirectory { get; set; }
    }
}
