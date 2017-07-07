using CommandLine;
using DigitalParadox.HandlebarsCli.Interfaces;

namespace DigitalParadox.HandlebarsCli.Verbs
{
    [Verb("test", HelpText = "Process handlebars Template")]
    public class TestVerb : IVerbDefinition
    {
        private readonly ILog _log;

        public TestVerb(ILog log)
        {
            _log = log;
        }
        
        public bool Verbose { get; set; }

        public int Execute()
        {
            _log.WriteInformation("test info");
            _log.WriteDebug("test debug");
            _log.WriteWarning("test write");
            _log.WriteVerbose("test verbose");
            _log.WriteError("test error");
            _log.WriteFatal("test error");

            return 0;
        }

    }
}
