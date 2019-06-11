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

namespace SSH.Application.Command.Commands.CutObject
{
    public class CutObjectCommandHandler : BaseCommandHandler, IRequestHandler<CutObjectCommand, CutObjectViewModel>
    {
        public CutObjectCommandHandler(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<CutObjectViewModel> Handle(CutObjectCommand request, CancellationToken cancellationToken)
        {
            bool alive = Ping(request.Credentials);
            if (alive)
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    var command = client.Cut(request.From, request.To, request.File);
                    return new CutObjectViewModel()
                    {
                        IsError = command.IsError,
                        Error = command.Error,
                        Nodes = command.Nodes
                    };
                }
            }
            else
            {
                return new CutObjectViewModel()
                {
                    IsError = true,
                    Error = "Couldnt connect to server"
                };
            }
        }
    }
}
