using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using HandlebarsCli.HandlebarsTemplateProcessor;
using Newtonsoft.Json;

namespace HandlebarsCli.Interfaces
{
    public interface ITemplateProcessorOptions
    {
        [JsonConverter(typeof(FileSystemInfoConverter<DirectoryInfo>))]
        [DefaultValue(".\\")]
        DirectoryInfo BaseDirectory { get; set; }
    }
}
