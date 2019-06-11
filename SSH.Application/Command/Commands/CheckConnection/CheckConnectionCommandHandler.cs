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

namespace SSH.Application.Connection.Command.CheckConnection
{
    public class CheckConnectionCommandHandler : BaseCommandHandler, IRequestHandler<CheckConnectionCommand, CheckConnectionViewModel>
    {
        public CheckConnectionCommandHandler(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<CheckConnectionViewModel> Handle(CheckConnectionCommand request, CancellationToken cancellationToken)
        {
            bool alive = Ping(request.Credentials);
            return new CheckConnectionViewModel()
            {
                IsAlive = alive,
                IsError = !alive,
                Error = alive ? "" : "Couldnt connect to server"
            };
        }
    }
}
