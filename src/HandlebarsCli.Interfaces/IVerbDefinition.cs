using HandlebarsCli.Plugins;

namespace HandlebarsCli.Interfaces
{
    public interface IVerbDefinition : IPlugin
    {
        bool Verbose { get; set; }
        int Execute();
    }
}