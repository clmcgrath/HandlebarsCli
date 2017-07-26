using System.Collections.Generic;
using DigitalParadox.Parsers.TemplateProcessor;
using HandlebarsCli.HandlebarsTemplateProcessor;
using Xunit;

using HandlebarsCli.Plugins;

namespace HandlebarsCli.Tests
{
    public class HandleBarsTemplateProcessorTests
    {
        [Fact]
        void TemplateProcessorReturnsValidTemplate()
        {
            // ReSharper disable once UnusedVariable
            ITemplateProcessor processor = new HandleBarsTemplateProcessor(new List<IHandlebarsHelper>(), new HandlebarsProcessorOptions( new ViewOptions()) );
        }
    }
}
