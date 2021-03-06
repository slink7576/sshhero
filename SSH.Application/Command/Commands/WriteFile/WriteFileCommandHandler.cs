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

namespace SSH.Application.Command.Commands.WriteFile
{
    public class WriteFileCommandHandler : BaseCommandHandler, IRequestHandler<WriteFileCommand, WriteFileViewModel>
    {
        public WriteFileCommandHandler(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<WriteFileViewModel> Handle(WriteFileCommand request, CancellationToken cancellationToken)
        {
            bool alive = Ping(request.Credentials);
            if (alive)
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    var response = client.WriteFile(request.Path, request.Data);
                    return new WriteFileViewModel()
                    {
                        IsError = response.IsError,
                        Error = response.Error
                    };
                }
            }
            else
            {
                return new WriteFileViewModel()
                {
                    IsError = true,
                    Error = "Couldnt connect to server"
                };
            }
        }
    }
}
