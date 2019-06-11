using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SSH.Application.Base;
using SSH.Core;
using SSH.Core.Commands;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSH.Application.Command.Commands.GetFiles
{
    public class GetFilesCommandHandler : BaseCommandHandler, IRequestHandler<GetFilesCommand, GetFilesViewModel>
    {
        public GetFilesCommandHandler(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<GetFilesViewModel> Handle(GetFilesCommand request, CancellationToken cancellationToken)
        {
            bool alive = Ping(request.Credentials);
            if (alive)
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    GetFilesCommandResponse response = null;
                    if (request.Path.Length == 0)
                    {
                        response = client.GetFiles();
                    }
                    else
                    {
                        response = client.GetFiles(request.Path);
                    }
                    return new GetFilesViewModel()
                    {
                        IsError = response.IsError,
                        Error = response.Error,
                        Nodes = response.Nodes,
                        Path = response.CurrentPath
                    };
                }
            }
            else
            {
                return new GetFilesViewModel()
                {
                    IsError = true,
                    Error = "Couldn connect to server"
                };
            }
        }
    }
}
