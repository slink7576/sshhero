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

namespace SSH.Application.Command.Commands.CopyObject
{
    public class CopyObjectCommandHandler : BaseCommandHandler, IRequestHandler<CopyObjectCommand, CopyObjectViewModel>
    {
        public CopyObjectCommandHandler(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<CopyObjectViewModel> Handle(CopyObjectCommand request, CancellationToken cancellationToken)
        {
            bool alive = Ping(request.Credentials);
            if (alive)
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    var command = client.Copy(request.From, request.To, request.File);
                    return new CopyObjectViewModel()
                    {
                        IsError = command.IsError,
                        Error = command.Error,
                        Nodes = command.Nodes
                    };
                }
            }
            else
            {
                return new CopyObjectViewModel()
                {
                    IsError = true,
                    Error = "Couldnt connect to server"
                };
            }
        }
    }
}
