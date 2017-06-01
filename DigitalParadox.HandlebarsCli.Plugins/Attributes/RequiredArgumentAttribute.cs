using System;
using System.Collections.Generic;

namespace DigitalParadox.Providers.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredArgumentAttribute : ArgummentValidationAttribute
    {
        public RequiredArgumentAttribute(string name)
        {
            
        }
        public Type Type { get; set; }
        public string Description { get; set; }
        public override bool Validate<T>(T obj, IDictionary<string, object>args)
        {

            return true;
        }
    }

    public  abstract class ArgummentValidationAttribute : Attribute
    {
        public abstract bool Validate<T>(T obj, IDictionary<string, object> args);
    }


}