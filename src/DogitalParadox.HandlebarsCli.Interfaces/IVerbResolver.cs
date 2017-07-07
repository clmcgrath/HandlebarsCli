using System.Collections.Generic;

namespace DigitalParadox.HandlebarsCli.Interfaces
{
    public interface IVerbResolver
    {
        IVerbDefinition Resolve(IEnumerable<string> args);
    }
}
