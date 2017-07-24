using System.Collections.Generic;
using DigitalParadox.HandlebarsCli.Interfaces;
using DigitalParadox.HandlebarsCli.Plugins;
using DigitalParadox.HandlebarsCli.Services.HandlebarsTemplateProcessor;
using Xunit;

namespace DigitalParadox.HandlebarsCli.Tests
{
    public class HandleBarsTemplateProcessorTests
    {
        [Fact]
        void TemplateProcessorReturnsValidTemplate()
        {
            ITemplateProcessor processor = new HandleBarsTemplateProcessor(new List<IHandlebarsHelper>(), new HandlebarsProcessorOptions( new ViewOptions()) );
        }
    }
}
