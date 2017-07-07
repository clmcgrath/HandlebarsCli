using DigitalParadox.HandlebarsCli.Plugins;

namespace DigitalParadox.HandlebarsCli.Interfaces
{
    public interface IVerbDefinition : IProvider
    {
        bool Verbose { get; set; }
        int Execute();
    }
}