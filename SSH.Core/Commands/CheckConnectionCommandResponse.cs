using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Core.Commands
{
    public class CheckConnectionCommandResponse : BaseCommandResponse
    {
        public bool IsConnected { get; set; }
    }
}
