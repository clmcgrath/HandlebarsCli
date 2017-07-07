using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using DigitalParadox.HandlebarsCli.Interfaces;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole;

namespace DigitalParadox.Logging
{
    public class Logger : ILog
    {
        public Logger(ILogger configuration)
        {
            Log.Logger = configuration;
        }
        public void Write(string message)
        {
            Log.Information(message);
        }

        public void WriteWarning(string message) => Log.Warning(message);
        public void WriteWarning(string message, Exception ex) => Log.Warning(message, ex);
        public void WriteVerbose(string message) => Log.Verbose(message);
        public void WriteInformation(string message) => Log.Information(message);
        public void WriteDebug(string message) => Log.Debug(message);
        public void WriteError(string message, Exception ex) => Log.Error(message, ex);
        public void WriteFatal(string message, Exception ex = null) => Log.Fatal(message, ex);
    }

    public class SerilogPlugin : UnityContainerExtension
    {
        protected override void Initialize()
        {
           Container.RegisterType<ILog,Logger>();
            
            var loggerConfig = new LoggerConfiguration();

            loggerConfig.
                WriteTo.ColoredConsole()
                .MinimumLevel.Is(LogEventLevel.Information);

            Container.RegisterInstance(loggerConfig);
            Container.RegisterInstance<ILogger>(Container.Resolve<LoggerConfiguration>().CreateLogger());
        }

    }
}
