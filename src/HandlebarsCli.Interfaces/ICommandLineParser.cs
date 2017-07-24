using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalParadox.HandlebarsCli.Interfaces
{
    public interface ICommandLineParser
    {
        IVerbDefinition Parse(IEnumerable<string> args);
    }
}
