using System;
using System.Collections.Generic;
using System.Text;
using SSH.Core.Entities;

namespace SSH.Core.Commands
{
    public class CreateObjectCommandResponse : BaseCommandResponse
    {
        public IEnumerable<FileNode> Nodes { get; set; }
    }
}
