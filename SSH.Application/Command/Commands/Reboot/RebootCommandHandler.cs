﻿using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SSH.Application.Base;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSH.Application.Command.Commands.Reboot
{
    public class RebootCommandHandler : BaseCommandHandler, IRequestHandler<RebootCommand, RebootViewModel>
    {
        public RebootCommandHandler(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<RebootViewModel> Handle(RebootCommand request, CancellationToken cancellationToken)
        {
            bool alive = Ping(request.Credentials);
            if (alive)
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    var response = client.Reboot();
                    return new RebootViewModel()
                    {
                        IsError = response.IsError,
                        Error = response.Error
                    };
                }
            }
            else
            {
                return new RebootViewModel()
                {
                    IsError = true,
                    Error = "Couldnt connect to server"
                };
            }
        }
    }
}
