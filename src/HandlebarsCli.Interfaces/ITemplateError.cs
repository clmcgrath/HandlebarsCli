﻿using System;

namespace HandlebarsCli.Interfaces
{
    public interface ITemplateError
    {
        string Description { get; set; }
        string Name { get; set; }
        Exception Exception { get; set; }
    }
}
