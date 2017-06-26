using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using DigitalParadox.HandlebarsCli.Utilities;

namespace DigitalParadox.HandlebarsCli.Verbs
{
    [Verb("process", HelpText = "Process handlebars Template")]
    public class Process : IVerbDefinition
    {
        public bool Verbose { get; set; }
        [Option('b', "basedir", Default = @".\", MetaValue = "Path", HelpText = "Base directory to load templates from")]
        public string TemplateDirectory { get; set; }

        [Option('i', "input", HelpText = "Input data file, supports json only at this time")]
        public string InputFile { get; set; }
        [Value(1, Required = true, MetaValue = "TEMPLATE NAME", HelpText = "Template Name to use for output, ie pscomment.template or pscomment.hbs")]

        public string TemplateName { get; set; }
        public int Execute()
        {
            Console.WriteLine("process verb executed");
            return 1;
        }
    }
}
