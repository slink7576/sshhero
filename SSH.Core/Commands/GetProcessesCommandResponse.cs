using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Core.Commands
{
    public class GetProcessesCommandResponse : BaseCommandResponse
    {
        public IEnumerable<ProcessInfo> Processes { get; set; }
    }
}
