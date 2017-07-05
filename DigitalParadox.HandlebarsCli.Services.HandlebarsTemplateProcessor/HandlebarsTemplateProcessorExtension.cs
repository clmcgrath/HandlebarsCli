using DigitalParadox.HandlebarsCli.Interfaces;
using Microsoft.Practices.Unity;

namespace DigitalParadox.HandlebarsCli.Services.HandlebarsTemplateProcessor
{
    public class HandlebarsTemplateProcessorExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {

            Container.RegisterType<ITemplateProcessor, HandleBarsTemplateProcessor>();
            
        }
    }

}
