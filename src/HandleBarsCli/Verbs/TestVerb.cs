using CommandLine;
using DigitalParadox.HandlebarsCli.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace DigitalParadox.HandlebarsCli.Verbs
{
    [Verb("test", HelpText = "Used for debugging purposes")]
    public class TestVerb : IVerbDefinition
    {
        public ILog Log { get; }

        [InjectionConstructor]
        public TestVerb(ILog log)
        {
            Log = log;
        }

        public TestVerb()
        {
            // only defined to work around clp bug 
        }

        public bool Verbose { get; set; }

        public int Execute()
        {
         

            Log.WriteInformation("test info");
            Log.WriteDebug("test debug");
            Log.WriteWarning("test write");
            Log.WriteVerbose("test verbose");
            Log.WriteError("test error");
            Log.WriteFatal("test error");

            return 0;
        }

    }
}
