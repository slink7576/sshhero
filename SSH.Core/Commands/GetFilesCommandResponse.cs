using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Core.Commands
{
    public class GetFilesCommandResponse : BaseCommandResponse
    {
        public string CurrentPath { get; set; }
        public IEnumerable<FileNode> Nodes { get; set; }
    }
}
