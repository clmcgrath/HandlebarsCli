using System.Collections.Generic;

namespace DigitalParadox.HandlebarsCli.Interfaces
{
    public interface ITemplateResult
    {
        string Result { get; set; }
        bool HasErrors { get; set; }
        ICollection<ITemplateError> Errors { get; set; }
    }
}
