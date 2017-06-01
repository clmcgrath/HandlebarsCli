using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;

namespace DigitalParadox.HandlebarsCli.Models
{
    public class Options
    {

        [Option('d', "data", Required = true, HelpText = "Json Input for binding template")]
        public string Data { get; set; }

        [Option('i', "input", Default = @".\Templates\comment.template", HelpText = "Input file to read.")]
        public string Template { get; set; }

        [Option('v', "verbose", Default = false, HelpText = "Display detailed output")]
        public bool Verbose { get; set; }

        public static Options Parse(IEnumerable<string> args )
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
            errorsList.ForEach(q =>
            {
                Console.Error.WriteLine();
            });

            //if (!errorsList.Any(q => q.StopsProcessing)) return;

            var exitCode = errorsList.First().Tag;
            Environment.Exit((int) exitCode);
        }

    }
}
