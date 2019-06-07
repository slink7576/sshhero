using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.CreateObject
{
    public class CreateObjectCommand : BaseCommand, IRequest<CreateObjectViewModel>
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public bool IsFile { get; set; }
    }
}
