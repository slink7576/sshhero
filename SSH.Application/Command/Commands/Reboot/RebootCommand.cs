using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.Reboot
{
    public class RebootCommand : BaseCommand, IRequest<bool>
    {
    }
}
