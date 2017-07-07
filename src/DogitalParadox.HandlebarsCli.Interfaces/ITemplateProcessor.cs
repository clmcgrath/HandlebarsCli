using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalParadox.HandlebarsCli.Interfaces
{
    public interface ITemplateProcessor
    {
        ITemplateProcessorOptions Options { get; set; }
        void BeforeProcess(string template, object data);
        void Initialize(string template, object data);
        ITemplateResult Process(string template, object data);
        void AfterProcess(string template, object data);
        void InitializeProject(DirectoryInfo target);
    }
}
