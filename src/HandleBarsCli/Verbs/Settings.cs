using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommandLine;
using DigitalParadox.Parsers.CommandLine;
using HandleBarsCLI.Models;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;

namespace HandleBarsCLI.Verbs
{
    [Verb("settings", HelpText = "Process handlebars Template")]
    public class Settings : IVerbDefinition
    {
        private readonly Configuration _config;

        public Settings()
        {
            _config = ServiceLocator.Current.GetInstance<Configuration>();
        }

        public Settings(Configuration config)
        {
            _config = config;
        }

        [Value(0, Default = SettingsAction.list)]
        public  SettingsAction Action { get; set; }
        
        public bool Verbose { get; set; }

        private int Get(string name)
        {
            _config.GetType()
                .GetProperties()
                .Where(q => String.Compare(name, q.Name, StringComparison.OrdinalIgnoreCase) == 0)
                .Select(q => new { q.Name, Value = q.GetValue(_config, null) })
                .ForEach(q => { RenderSetting(q.Name, q.Value);});

            return 0;
        }

        private int Set(string name, object value)
        {
            Console.WriteLine($"set {name} to {value} executed");
            return 0;
        }

        private int List()
        {
            _config.GetType()
                .GetProperties()
                .Select(q=>new { q.Name, Value = q.GetValue(_config, null) })
                .ForEach(q =>
                {
                    RenderSetting(q.Name, q.Value);
                });
            return 0;
        }
        void RenderSetting(string name, object value)
        {
            Console.Write($"{ name } : ");
            if (value is IEnumerable)
            {
                Console.WriteLine("{");
                ((IEnumerable<object>) value).ForEach(Console.WriteLine);
                Console.WriteLine("}");
            }
            else
            {
                Console.Write(value);
            }
        }

        public int Execute()
        {
            switch (Action)
            {
                case SettingsAction.get:
                    return Get(Name);
                    
                case SettingsAction.set:
                    return Set(Name, Value);
                    
                case SettingsAction.list:
                    return List();
                default:
                    throw new InvalidEnumArgumentException(nameof(Action), (int)Action, typeof(SettingsAction));
            }
            
        }

        [Value(1)]
        public string Name { get; set; }

        [Value(2)]
        public string Value { get; set; }

    }

    public enum SettingsAction
    {
        get, set, list
    }
}
