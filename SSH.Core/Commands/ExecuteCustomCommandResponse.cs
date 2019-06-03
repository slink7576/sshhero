using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Core.Commands
{
    public class ExecuteCustomCommandResponse : BaseCommandResponse
    {
        public string Result { get; set; }
    }
}
