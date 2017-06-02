using System;
using System.Linq;

namespace DigitalParadox.HandlebarsCli.Plugins.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, Inherited = false)]
    public class PluginInfoAttribute : Attribute
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public HelperType HelperType { get; set; } = HelperType.Inline;

        public string GetDisplayName(object obj, string template)
        {
            var displayName = DisplayName;

            obj.GetType().GetProperties().ToList().ForEach(prop =>
            {
                string val;
                try
                {
                    val = prop.GetValue(obj).ToString();
                }
                catch (Exception e)
                {
                    val = e.GetType().Name;
                }
                displayName = displayName.Replace($"{{{prop.Name}}}", val.ToString());
            });
            return displayName;
        }
    }
}