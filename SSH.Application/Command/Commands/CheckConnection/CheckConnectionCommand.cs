using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Connection.Command.CheckConnection
{
    public class CheckConnectionCommand : BaseCommand, IRequest<CheckConnectionViewModel>
    {
    }
}
