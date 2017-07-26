using System;
using System.IO;
using DigitalParadox.HandlebarsCli.Interfaces;

namespace HandlebarsCli.Verbs.BuiltIn
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
