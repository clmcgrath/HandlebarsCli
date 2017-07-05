using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalParadox.HandlebarsCli.Interfaces
{
    public interface ITemplateProcessor
    {
        ITemplateResult Process(string template, object data);
    }
}
