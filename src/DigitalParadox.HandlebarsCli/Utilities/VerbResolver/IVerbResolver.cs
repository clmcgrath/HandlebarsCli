using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalParadox.HandlebarsCli.Utilities
{
    public interface IVerbResolver
    {
        IVerbDefinition Resolve(IEnumerable<string> args);
    }
}
