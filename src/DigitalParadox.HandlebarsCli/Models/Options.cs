using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;

namespace DigitalParadox.HandlebarsCli.Models
{
    public class Options
    {   
        [Option('d', "data", Default = "", HelpText = "Json Input for binding template, supports json only at this time")]
        public string Data { get; set; }

        [Option('b', "basedir", Default = @".\", MetaValue = "Path", HelpText = "Base directory to load templates from")]
        public string TemplateDirectory { get; set; }

        [Value(1, Required = true, MetaValue = "TEMPLATE NAME", HelpText = "Template Name to use for output, ie pscomment.template or pscomment.hbs")]
        public string TemplateName { get; set; }


        [Option('v', "views", MetaValue = "PATH", Default = @"{{TemplateDirectory}}\Views",
            HelpText = "directory to find view templates, Defaults to {TemplateDirectory}\\Views")]
        public string ViewsDirectory { get; set; }

        [Option('p', "partials", MetaValue = "PATH", Default = @"{{TemplateDirectory}}\Views\\partials",
            HelpText = "directory to find view templates, Defaults to {CurrentTemplateParent}\\partials")]
        public string PartialsDirectory { get; set; }
        
        [Option('i', "input", HelpText = "Input data file, supports json only at this time")]
        public string InputFile { get; set; }

        [Option('v', "verbose", Default = false, HelpText = "Display detailed output")]
        public bool Verbose { get; set; }

        [Option('o', "output",  HelpText = "File path to output rendered text, Default: Console.Out")]
        public string OutputFile { get; set; }

        public static Options Parse(IEnumerable<string> args)
        {
            var parser = new Parser(with =>
            {
                with.EnableDashDash = true;
                with.HelpWriter = Console.Out;
                with.CaseInsensitiveEnumValues = true;
            });

            Options options = null;
            var result = parser.ParseArguments<Options>(args)
                .WithParsed(opts => { options = opts; })
                .WithNotParsed(DisplayError);

            return options;
        }

        private static void DisplayError(IEnumerable<Error> errors)
        {
            var errorsList = errors as List<Error> ?? errors.ToList();
            errorsList.ForEach(q => { Console.Error.WriteLine(); });

            //if (!errorsList.Any(q => q.StopsProcessing)) return;

            var exitCode = errorsList.First().Tag;
            Environment.Exit((int) exitCode);
        }
    }
}