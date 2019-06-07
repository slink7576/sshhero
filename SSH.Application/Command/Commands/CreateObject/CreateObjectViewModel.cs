using SSH.Application.Base;
using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.CreateObject
{
    public class CreateObjectViewModel : BaseViewModel
    {
        public IEnumerable<FileNode> Nodes { get; set; }
    }
}
