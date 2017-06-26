using DigitalParadox.HandlebarsCli.Plugins;

namespace DigitalParadox.HandlebarsCli.Utilities
{
    public interface IVerbDefinition : IProvider
    {
        bool Verbose { get; set; }
        int Execute();
    }
}