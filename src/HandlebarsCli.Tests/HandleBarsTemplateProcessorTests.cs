using System.Collections.Generic;
using Xunit;
using HandlebarsCli.HandlebarsTemplateProcessor;
using HandlebarsCli.Plugins;
using HandlebarsCli.Interfaces;

namespace HandlebarsCli.Tests
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
