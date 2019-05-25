using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.System.Commands.GetSystemInfo
{
    public class GetSystemInfoCommand : BaseCommand, IRequest<SystemInfoViewModel>
    {
    }
}
