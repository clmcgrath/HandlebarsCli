using DigitalParadox.HandlebarsCli.Plugins;
using DigitalParadox.HandlebarsCli.Interfaces;
using HandlebarsDotNet;
using Microsoft.Practices.ObjectBuilder2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalParadox.HandlebarsCli.Services.HandlebarsTemplateProcessor
{

    public class HandleBarsTemplateProcessor : ITemplateProcessor
    {
        public HandleBarsTemplateProcessor(ICollection<IHandlebarsHelper> plugins, ITemplateProcessorOptions configuration )
        {
            Plugins = plugins;
            
            Plugins.ForEach(p => Handlebars.RegisterHelper(
                p.Name,
                (writer, options, context, arguments) => p.Execute(writer, options, context, arguments)));

            Handlebars.Configuration.ThrowOnUnresolvedBindingExpression = true;

            var viewsDir = configuration.ViewsDirectory.Replace("{{TemplateDirectory}}", configuration.TemplateDirectory);

            var templates = Directory.GetFiles(viewsDir, "*.hbs", SearchOption.AllDirectories).Select(tpl => new FileInfo(tpl));

            templates.ForEach(tpl => Handlebars.RegisterTemplate(tpl.Name.TrimEnd(tpl.Extension.ToCharArray()), File.ReadAllText(tpl.FullName)));

        }

        public interface ITemplateProcessorOptions
        {
            string ViewsDirectory { get; set; }
            string TemplateDirectory { get; set; }
        }

        public ITemplateResult Process(string template, object data)
        {
            return null;
        }
        public ICollection<IHandlebarsHelper> Plugins { get; set; }
    }

}
