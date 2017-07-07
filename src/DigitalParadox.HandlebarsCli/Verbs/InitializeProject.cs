using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using DigitalParadox.HandlebarsCli.Models;
using DigitalParadox.HandlebarsCli.Plugins;
using DigitalParadox.HandlebarsCli.Utilities;
using HandlebarsDotNet;
using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;
using Dependency = Microsoft.Practices.Unity.DependencyAttribute;
using DigitalParadox.HandlebarsCli.Interfaces;
using DigitalParadox.Parsing.CommandLine;

namespace DigitalParadox.HandlebarsCli.Verbs
{
    [Verb("init", HelpText = "Process handlebars Template")]
    public class InitializeProject : IVerbDefinition
    {
        private readonly ITemplateProcessor _processor;
        private readonly ILog _log;


        public InitializeProject(ITemplateProcessor processor, ILog log)
        {
            _processor = processor;
            _log = log;
        }
        
        public bool Verbose { get; set; }
        [Option('b', "basedir", Default = @".\", MetaValue = "Path", HelpText = "Base directory to load templates from")]
        public string TemplateDirectory { get; set; }
        




        public int Execute()
        {
            
            try
            {
                _processor.InitializeProject(new DirectoryInfo(TemplateDirectory));
                return 0;
            }
            catch (Exception e)
            {
               _log.WriteError("Project Initialization Failed", e);
                return e.HResult;
            }

        }

        public string ViewsDirectory { get; set; }

        public string OutputFile { get; set; }

        public string Data { get; set; }
    }
}
