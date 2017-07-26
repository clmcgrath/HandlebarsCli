using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using CommandLine.Text;
using DigitalParadox.Parsers.CommandLine;

namespace Parsers.CommandLine
{
    public class VerbResolver : IVerbResolver
    {
        private readonly Parser _parser;
        private readonly IEnumerable<IVerbDefinition> _verbs;

        public VerbResolver(Parser parser, IEnumerable<IVerbDefinition> verbs)
        {
            _parser = parser;
            _verbs = verbs;
        }

        public IVerbDefinition Resolve(IEnumerable<string> args)
        {
            
            IVerbDefinition command = null;

            var parse =
                _parser.ParseArguments(args, _verbs.Select(q => q.GetType()).ToArray());
                

                parse.WithParsed(options =>
                {
                    command = (IVerbDefinition)options;
                });
                
                
                parse.WithNotParsed(errors =>
                {
                    Console.Error.WriteLine(HelpText.AutoBuild(parse).ToString());
                    Console.WriteLine("Brutalize your favourite key to exit.");
                    Console.ReadKey();
                    Environment.Exit(errors.Count());
                });

                
            return command;
        }


    }
}
