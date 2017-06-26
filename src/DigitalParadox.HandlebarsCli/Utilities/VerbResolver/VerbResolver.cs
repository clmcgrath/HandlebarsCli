﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using Microsoft.Practices.Unity;

namespace DigitalParadox.HandlebarsCli.Utilities
{
    public class VerbResolver : IVerbResolver
    {
        private readonly IUnityContainer _container;
        private readonly IEnumerable<IVerbDefinition> _verbs;

        public VerbResolver(IUnityContainer container)
        {
            _container = container;
            _verbs = container.ResolveAll<IVerbDefinition>();
        }

        public IVerbDefinition Resolve(IEnumerable<string> args)
        {

            var parser = new Parser();

            IVerbDefinition command = null;

            var parse =
                parser.ParseArguments(args.Skip(1), _verbs.Select(q => q.GetType()).ToArray());
                
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
                        //setup error resource for error strings ...
                        Console.WriteLine($"Error: { error.Tag }");
                        Environment.Exit((int)error.Tag);
                        //Exception
                    }

                });


            
            return command;
        }


    }
}