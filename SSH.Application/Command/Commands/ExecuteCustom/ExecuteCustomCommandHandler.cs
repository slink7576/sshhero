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

namespace SSH.Application.Command.Commands.ExecuteCustom
{
    public class ExecuteCustomCommandHandler : BaseCommandHandler, IRequestHandler<ExecuteCustomCommand, ExecuteCustomViewModel>
    {
        public ExecuteCustomCommandHandler(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        public async Task<ExecuteCustomViewModel> Handle(ExecuteCustomCommand request, CancellationToken cancellationToken)
        {
            bool alive = Ping(request.Credentials);
            if (alive)
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    var command = client.Execute(request.Command, request.IsSudo);
                    return new ExecuteCustomViewModel()
                    {                    
                        IsError = command.IsError,
                        Error = command.Error,
                        Result = command.Result
                    };
                }
            }
            else
            {
                return new ExecuteCustomViewModel()
                {
                    IsError = true,
                    Error = "Couldnt connect to server"
                };
            }
        }
    }
}

