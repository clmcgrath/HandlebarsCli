using System.Collections.Generic;
using System.Linq;
using DigitalParadox.HandlebarsCli.Interfaces;

namespace DigitalParadox.HandlebarsCli.Services.HandlebarsTemplateProcessor
{
    public class HandlebarsTemplateResult : ITemplateResult
    {
        public HandlebarsTemplateResult(string result, ICollection<ITemplateError> errors)
        {
            Errors = errors;
            Result = result;
        }

        public HandlebarsTemplateResult(string result)
        {
            Errors = new List<ITemplateError>();
            Result = result;
        }

        public string Result { get; }
        public bool HasErrors => Errors.Any();
        public ICollection<ITemplateError> Errors { get; }
    }
}