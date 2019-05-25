using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSH.Application.System.Commands.GetSystemInfo
{
    public class GetSystemInfoCommandHandler : IRequestHandler<GetSystemInfoCommand, SystemInfoViewModel>
    {
        public async Task<SystemInfoViewModel> Handle(GetSystemInfoCommand request, CancellationToken cancellationToken)
        {
            using (var client = new SSHClient(request.Credentials))
            {
                return new SystemInfoViewModel() { OS = client.GetInfo().Os };
            }
        }
    }
}
