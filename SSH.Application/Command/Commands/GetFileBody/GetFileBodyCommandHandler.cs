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

namespace SSH.Application.Command.Commands.GetFileBody
{
    public class GetFileBodyCommandHandler : BaseCommandHandler, IRequestHandler<GetFileBodyCommand, GetFileBodyViewModel>
    {
        public GetFileBodyCommandHandler(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<GetFileBodyViewModel> Handle(GetFileBodyCommand request, CancellationToken cancellationToken)
        {
            bool alive = Ping(request.Credentials);
            if (alive)
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    var command = client.GetFile(request.Path);
                    return new GetFileBodyViewModel()
                    {
                        IsError = command.IsError,
                        Error = command.Error,
                        Data = command.Data
                    };
                }
            }
            else
            {
                return new GetFileBodyViewModel()
                {
                    IsError = true,
                    Error = "Couldnt connect to server"
                };
            }
        }
    }
}
