﻿using System;
using DigitalParadox.Parsers.TemplateProcessor;

namespace HandlebarsCli.HandlebarsTemplateProcessor
{
    public class HandlebarsTemplateError: ITemplateError
    {
        public HandlebarsTemplateError(Exception e)
        {
            Exception = e;
            Name = e.GetType().Name;
            Description = e.Message;
        }

        public string Description { get; set; }
        public string Name { get; set; }
        public Exception Exception { get; set; }
    }
}