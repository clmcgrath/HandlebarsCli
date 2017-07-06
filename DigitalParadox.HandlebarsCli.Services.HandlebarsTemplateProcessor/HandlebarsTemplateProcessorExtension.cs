﻿using System.ComponentModel;
using System.IO;
using DigitalParadox.HandlebarsCli.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;

namespace DigitalParadox.HandlebarsCli.Services.HandlebarsTemplateProcessor
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
        public DirectoryInfo BaseDirectory { get; set; } = new DirectoryInfo(".\\");
        public ViewOptions ViewOptions { get; set; }
        
        
    }
    public class ViewOptions
    {
        public bool RelativePathNaming { get; set; } = true;
        public bool IncludeShortName { get; set; } = true;
        [JsonConverter(typeof(FileSystemInfoConverter<DirectoryInfo>))]
        public DirectoryInfo Directory { get; set; } = new DirectoryInfo(".\\Views\\");
    }
}
