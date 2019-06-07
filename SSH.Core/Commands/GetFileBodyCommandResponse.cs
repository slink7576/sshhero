using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Core.Commands
{
    public class GetFileBodyCommandResponse : BaseCommandResponse
    {
        public string Data { get; set; }
    }
}
