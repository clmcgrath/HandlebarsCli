using System.Collections.Generic;

namespace HandlebarsCli.Interfaces
{
    public interface IVerbResolver
    {
        IVerbDefinition Resolve(IEnumerable<string> args);
    }
}
