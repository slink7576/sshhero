using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSH.Application.Command.Commands.ExecuteCustom
{
    public class ExecuteCustomCommandHandler : IRequestHandler<ExecuteCustomCommand, ExecuteCustomCommandViewModel>
    {
        public async Task<ExecuteCustomCommandViewModel> Handle(ExecuteCustomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ping = new Ping();
                PingReply pingresult = ping.Send(request.Credentials.Hostname);
                if (pingresult.Status.ToString() == "Success")
                {
                    using (var client = new SSHClient(request.Credentials))
                    {
                        var command = client.Execute(request.Command, request.IsSudo);
                        return new ExecuteCustomCommandViewModel()
                        {
                            Result = command.Result,
                            Error = command.Error,
                            IsError = command.Error.Length == 0 ? false : true
                        };
                    }
                }
                else
                {
                    return new ExecuteCustomCommandViewModel() { IsError = true};
                }
               
            }
            catch (Exception c)
            {
                return new ExecuteCustomCommandViewModel() { Error = c.Message, IsError = true };

            }
        }
    }
}

