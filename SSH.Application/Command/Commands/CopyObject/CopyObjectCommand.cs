using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.CopyObject
{
    public class CopyObjectCommand : BaseCommand, IRequest<CopyObjectViewModel>
    {
        public string From { get; set; }
        public string To { get; set; }
        public string File { get; set; }
    }
}
