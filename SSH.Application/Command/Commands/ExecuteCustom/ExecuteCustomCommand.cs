using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.ExecuteCustom
{
    public class ExecuteCustomCommand : BaseCommand, IRequest<ExecuteCustomCommandViewModel>
    {
        public string Command { get; set; }
    }
}
