using System.Collections.Generic;
using DigitalParadox.HandlebarsCli.Plugins;
using Xunit;
using DigitalParadox.HandlebarsCli.Interfaces;
using DigitalParadox.HandlebarsCli.Services.HandlebarsTemplateProcessor;

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
