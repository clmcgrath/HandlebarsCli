using System.Collections.Generic;

namespace HandlebarsCli.Interfaces
{
    public interface ITemplateResult
    {
        string Result { get; }
        bool HasErrors { get; }
        ICollection<ITemplateError> Errors { get; }

    }
}
