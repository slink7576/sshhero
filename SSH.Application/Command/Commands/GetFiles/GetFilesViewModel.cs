using SSH.Application.Base;
using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.GetFiles
{
    public class GetFilesViewModel : BaseViewModel
    {
        public IEnumerable<FileNode> Nodes { get; set; }
        public string Path { get; set; }
    }
}
