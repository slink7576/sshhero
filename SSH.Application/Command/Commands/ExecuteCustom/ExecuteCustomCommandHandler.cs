using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
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
            catch (Exception c)
            {
                return new ExecuteCustomCommandViewModel() { Error = c.Message, IsError = true };

            }
        }
    }
}

