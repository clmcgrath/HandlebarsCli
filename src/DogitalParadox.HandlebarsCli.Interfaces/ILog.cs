using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalParadox.HandlebarsCli.Interfaces
{
    public interface ILog
    {
        void WriteWarning(string message);
        void WriteVerbose(string message);
        void WriteInformation(string message);
        void WriteDebug(string message);
        void WriteError(string message, Exception ex = null);
        void WriteFatal(string message, Exception ex = null);
    }
}
