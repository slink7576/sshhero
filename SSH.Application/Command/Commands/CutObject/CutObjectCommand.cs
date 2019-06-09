using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.CutObject
{
    public class CutObjectCommand : BaseCommand, IRequest<CutObjectViewModel>
    {
        public string From { get; set; }
        public string To { get; set; }
        public string File { get; set; }
    }
}
