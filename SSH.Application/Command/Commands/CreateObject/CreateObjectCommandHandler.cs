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

namespace SSH.Application.Command.Commands.CreateObject
{
    public class CreateObjectCommandHandler : BaseCommandHandler, IRequestHandler<CreateObjectCommand, CreateObjectViewModel>
    {
        public CreateObjectCommandHandler(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<CreateObjectViewModel> Handle(CreateObjectCommand request, CancellationToken cancellationToken)
        {
            bool alive = Ping(request.Credentials);
            if (alive)
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    var command = client.Create(request.Path, request.Name, request.IsFile);
                    return new CreateObjectViewModel()
                    {
                        IsError = command.IsError,
                        Error = command.Error,
                        Nodes = command.Nodes
                    };
                }
            }
            else
            {
                return new CreateObjectViewModel()
                {
                    IsError = true,
                    Error = "Couldnt connect to server"
                };
            }
        }
    }
}
