using DigitalParadox.HandlebarsCli.Plugins;
using DigitalParadox.HandlebarsCli.Interfaces;
using HandlebarsDotNet;
using Microsoft.Practices.ObjectBuilder2;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HandlebarsCli.Plugins;
using HandlebarsCli.Interfaces;

namespace HandlebarsCli.HandlebarsTemplateProcessor
{

    public class HandleBarsTemplateProcessor : ITemplateProcessor
    {
        public HandleBarsTemplateProcessor(ICollection<IHandlebarsHelper> plugins, ITemplateProcessorOptions configuration )
        {
            
            var conf = (HandlebarsProcessorOptions) configuration;

            Plugins = plugins;
            
            Plugins.ForEach(p => Handlebars.RegisterHelper(
                p.Name,
                (writer, options, context, arguments) => p.Execute(writer, options, context, arguments)));

            Handlebars.Configuration.ThrowOnUnresolvedBindingExpression = true;

            var viewsDir = conf.ViewOptions.Directory.FullName.Replace("{{BaseDirectory}}", conf.DefaultBaseDirectory.FullName);

            var templates = Directory.GetFiles(viewsDir, "*.hbs", SearchOption.AllDirectories).Select(tpl => new FileInfo(tpl));

            templates.ForEach(tpl => Handlebars.RegisterTemplate(tpl.Name.TrimEnd(tpl.Extension.ToCharArray()), File.ReadAllText(tpl.FullName)));

        }


        public ITemplateProcessorOptions Options { get; set; }

        public void BeforeProcess(string template, object data)
        {

        }

        public void Initialize(string template, object data)
        {
            
        }

        public ITemplateResult Process(string template, object data)
        {
            Initialize(template, data);
            BeforeProcess(template, data);

            var compiledTemplate = Handlebars.Compile(template);

            try
            {
                var result = compiledTemplate(data);

                AfterProcess(result, data);

                return new HandlebarsTemplateResult(result);
            }
            catch (HandlebarsCompilerException e)
            {
                var errorResult = new HandlebarsTemplateResult(template);
                errorResult.Errors.Add(new HandlebarsTemplateError(e));
                return errorResult;
            }
        }

        public void AfterProcess(string template, object data)
        {
            
        }

        public void InitializeProject(DirectoryInfo target) => InitializeProject(target, false);

        public void InitializeProject(DirectoryInfo target, bool cleanDirectory)
        {
            if (target.Exists)
            {
                if (cleanDirectory)
                {
                    target.GetFileSystemInfos().ForEach(fsi=> 
                        fsi.Delete()
                    );

                }

                Directory.CreateDirectory(Path.Combine(target.FullName, "Views"));
                File.WriteAllText(Path.Combine(target.FullName, "main_template.hbs"), "My New Project Template\n {{#names}} \n\t{{> name }} \n{{/names}}");

                File.WriteAllText(Path.Combine(target.FullName, "Views\\name.hbs"), "Hello {{this}}!! \n This Is An Example View ");
                var data = new {names = new[] {"Chris", "Mike", "Tony"}};

                //File.WriteAllText(Path.Combine(target.FullName, "data.yaml"), new Serializer().Serialize(data));
                //File.WriteAllText(Path.Combine(target.FullName, "data.json"), JsonConvert.SerializeObject(data));
                
            }
            else
            {
                throw new DirectoryNotFoundException($"{target.FullName} does not exist.");
            }
        }


        public ICollection<IHandlebarsHelper> Plugins { get; set; }
    }
}
