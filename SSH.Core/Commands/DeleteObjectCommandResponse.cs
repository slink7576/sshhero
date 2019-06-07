using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Core.Commands
{
    public class DeleteObjectCommandResponse : BaseCommandResponse
    {
        public IEnumerable<FileNode> Nodes { get; set; }
    }
}
