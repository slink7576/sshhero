using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Microsoft.Extensions.Caching.Memory;
using SSH.Application.Base;

namespace SSH.Application.System.Commands.GetSystemInfo
{
    public class GetSystemInfoCommandHandler : BaseCommandHandler, IRequestHandler<GetSystemInfoCommand, SystemInfoViewModel>
    {
        public GetSystemInfoCommandHandler(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<SystemInfoViewModel> Handle(GetSystemInfoCommand request, CancellationToken cancellationToken)
        {
            bool alive = Ping(request.Credentials);
            if (alive)
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    var response = client.GetInfo();
                    return new SystemInfoViewModel()
                    {
                        IsError = response.IsError,
                        Error = response.Error,
                        OS = response.Information.Os
                    };
                }
            }
            else
            {
                return new SystemInfoViewModel()
                {
                    IsError = true,
                    OS = null,
                    Error = "Couldnt connect to server"
                };
            }
        }
    }
}
