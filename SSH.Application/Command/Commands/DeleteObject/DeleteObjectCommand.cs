using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.DeleteObject
{
    public class DeleteObjectCommand : BaseCommand, IRequest<DeleteObjectViewModel>
    {
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
