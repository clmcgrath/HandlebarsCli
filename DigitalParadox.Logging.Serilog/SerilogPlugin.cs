using System.Collections.Generic;
using DigitalParadox.HandlebarsCli.Interfaces;
using Microsoft.Practices.Unity;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DigitalParadox.Logging.Serilogger
{
    public partial class SerilogPlugin : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<ILogTemplate, LogTemplate>();
            
            Container.RegisterInstance(LogEventLevel.Information);

            Container.RegisterType<LoggerConfiguration>( new InjectionFactory(container => 
                new LoggerConfiguration()
                .WriteTo.ColoredConsole(outputTemplate:Container.Resolve<ILogTemplate>().Template)
                .MinimumLevel.Is(Container.Resolve<LogEventLevel>())));

            Container.RegisterType<IEnumerable<ILogEventSink>>();
            
            Container.RegisterType<ILogger>(new InjectionFactory(container => Container.Resolve<LoggerConfiguration>().CreateLogger()));

            Container.RegisterType<ILog, SerilogLogger>();
        }
    }
}
