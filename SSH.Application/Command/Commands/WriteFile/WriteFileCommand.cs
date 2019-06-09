using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.WriteFile
{
    public class WriteFileCommand : BaseCommand, IRequest<WriteFileViewModel>
    {
        public string Path { get; set; }
        public string Data { get; set; }
    }
}
