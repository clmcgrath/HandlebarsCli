using System;
using System.Linq;

namespace DigitalParadox.Providers.Actions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, Inherited = false)]
    public class ProviderInfoAttribute : Attribute
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

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
