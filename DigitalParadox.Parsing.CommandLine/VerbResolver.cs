using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using CommandLine.Text;
using DigitalParadox.HandlebarsCli.Interfaces;

namespace DigitalParadox.Parsing.CommandLine
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
                _parser.ParseArguments(args.Skip(1), _verbs.Select(q => q.GetType()).ToArray<Type>());
                
                foreach (var verb in _verbs)
                {
                    parse.WithParsed((options) =>
                    {
                        command = (IVerbDefinition)options;
                    });
                }
                
                parse.WithNotParsed(errors =>
                {
                    foreach (var error in errors)
                    {
                    
                        //Exception
                        Console.Error.WriteLine(HelpText.AutoBuild(parse).ToString());
                        Console.WriteLine("Brutalize a key with your favourite finger to exit.");
                        Console.ReadKey();
                        Environment.Exit((int)error.Tag);
                    }

                });

                
            return command;
        }


    }
}
