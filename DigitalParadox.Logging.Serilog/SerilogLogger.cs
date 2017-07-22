using System;
using DigitalParadox.HandlebarsCli.Interfaces;
using Serilog;

namespace DigitalParadox.Logging.Serilogger
{
    public class SerilogLogger : ILog
    {
        public SerilogLogger(ILogger log)
        {
            Log.Logger = log;
        }

        public void Write(string message) => Log.Information(message);

        public void WriteWarning(string message) => Log.Warning(message);
        public void WriteWarning(string message, Exception ex) => Log.Warning(message, ex);
        public void WriteVerbose(string message) => Log.Verbose(message);
        public void WriteInformation(string message) => Log.Information(message);
        public void WriteDebug(string message) => Log.Debug(message);
        public void WriteError(string message, Exception ex) => Log.Error(message, ex);
        public void WriteFatal(string message, Exception ex = null) => Log.Fatal(message, ex);
    }
}