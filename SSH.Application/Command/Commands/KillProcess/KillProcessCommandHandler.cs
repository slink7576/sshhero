using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SSH.Application.Base;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSH.Application.Command.Commands.KillProcess
{
    public class KillProcessCommandHandler : BaseCommandHandler, IRequestHandler<KillProcessCommand, KillProcessViewModel>
    {
        public KillProcessCommandHandler(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<KillProcessViewModel> Handle(KillProcessCommand request, CancellationToken cancellationToken)
        {
            bool alive = Ping(request.Credentials);
            if (alive)
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    var response = client.KillProcess(request.Id);
                    return new KillProcessViewModel()
                    {
                        IsError = response.IsError,
                        Error = response.Error
                    };
                }
            }
            else
            {
                return new KillProcessViewModel()
                {
                    IsError = true,
                    Error = "Couldnt connect to server"
                };
            }
        }
    }
}
