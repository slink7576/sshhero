using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Core.Commands
{
    public abstract class BaseCommandResponse
    {
        public bool IsError { get; set; }
        public string Error { get; set; }
    }
}
