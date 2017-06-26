using DigitalParadox.HandlebarsCli.Utilities;
using Microsoft.Practices.Unity;

namespace DigitalParadox.HandlebarsCli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var bootstrapper = new Bootstrapper())
            {

                bootstrapper.Setup();
                var verb = bootstrapper.Resolve<IVerbDefinition>();
                verb.Execute();

            }
        }
    }
}