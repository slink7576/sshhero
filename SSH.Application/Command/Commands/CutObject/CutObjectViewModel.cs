using SSH.Application.Base;
using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.CutObject
{
    public class CutObjectViewModel : BaseViewModel
    {
        public IEnumerable<FileNode> Nodes { get; set; }
    }
}
