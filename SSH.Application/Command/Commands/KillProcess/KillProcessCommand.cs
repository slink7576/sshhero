using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.KillProcess
{
    public class KillProcessCommand : BaseCommand, IRequest<KillProcessViewModel>
    {
        public int Id { get; set; }
    }
}
