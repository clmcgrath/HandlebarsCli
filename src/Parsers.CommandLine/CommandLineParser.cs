﻿using DigitalParadox.HandlebarsCli.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalParadox.Parsing.CommandLineParser
{
    public class CommandLineParser : ICommandLineParser
    {
        private readonly IVerbResolver _resolver;

        public CommandLineParser(IVerbResolver resolver)
        {
            _resolver = resolver;
        }
        public IVerbDefinition Parse(IEnumerable<string> args)
        {
            return _resolver.Resolve(args);
        }
    }
}