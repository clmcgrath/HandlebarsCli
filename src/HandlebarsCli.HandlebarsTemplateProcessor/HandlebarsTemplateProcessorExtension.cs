using System.IO;
using DigitalParadox.Parsers.Json.Converters;
using DigitalParadox.Parsers.TemplateProcessor;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;


namespace HandlebarsCli.HandlebarsTemplateProcessor
{
    public class HandlebarsTemplateProcessorExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<ITemplateProcessor, HandleBarsTemplateProcessor>();
            Container.RegisterType<ITemplateProcessorOptions, HandlebarsProcessorOptions>();
        }

    }

    public class HandlebarsProcessorOptions :ITemplateProcessorOptions
    {
        public HandlebarsProcessorOptions(ViewOptions viewOptions)
        {
            ViewOptions = viewOptions;
        }
        [JsonConverter(typeof(FileSystemInfoConverter<DirectoryInfo>))]
        public DirectoryInfo DefaultBaseDirectory { get; set; } = new DirectoryInfo(".\\");
        public ViewOptions ViewOptions { get; set; }


        public DirectoryInfo BaseDirectory { get; set; }
    }
    public class ViewOptions
    {
        public bool RelativePathNaming { get; set; } = true;
        public bool IncludeShortName { get; set; } = true;
        [JsonConverter(typeof(FileSystemInfoConverter<DirectoryInfo>))]
        public DirectoryInfo Directory { get; set; } = new DirectoryInfo(".\\Views\\");
    }
}
