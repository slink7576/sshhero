using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Core.Commands
{
    public class GetSystemInfoCommandResponse : BaseCommandResponse
    {
        public SystemInfo Information { get; set; }
    }
}
