using System;
using System.Collections.Generic;

namespace DigitalParadox.Providers.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    internal class OptionalArgumentAttribute : Attribute
    {
        public OptionalArgumentAttribute(string name)
        {
            Name = name;
        }

        public Type Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool  Validate(Dictionary<string, string> args, bool throwException = true )
        {
            var isValid = false;
            if (!args.ContainsKey(Name))
            {
                if (throwException)
                {
                    throw new ArgumentException($"The {Name} Argument is required");
                }
                
            }
            else
            {
                isValid = true;
            }

            if (Type != null)
            {
                isValid = args[Name].GetType() == Type;
            }

            return isValid;
        }
    }
}