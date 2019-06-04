using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.GetFiles
{
    public class GetFilesCommand : BaseCommand, IRequest<GetFilesViewModel>
    {
        public string Path { get; set; }
    }
}
