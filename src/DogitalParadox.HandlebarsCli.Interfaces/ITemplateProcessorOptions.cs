using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalParadox.HandlebarsCli.Services.HandlebarsTemplateProcessor;
using Newtonsoft.Json;

namespace DigitalParadox.HandlebarsCli.Interfaces
{
    public interface ITemplateProcessorOptions
    {
        [JsonConverter(typeof(FileSystemInfoConverter<DirectoryInfo>))]
        [DefaultValue(".\\")]
        DirectoryInfo BaseDirectory { get; set; }
    }
}
