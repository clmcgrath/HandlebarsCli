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

                var app = bootstrapper.Resolve<Application>();
                app.Run();
            }
        }
    }
}