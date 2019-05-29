using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSH.Application.Connection.Command.CheckConnection
{
    public class CheckConnectionCommandHandler : IRequestHandler<CheckConnectionCommand, CheckConnectionViewModel>
    {
        public async Task<CheckConnectionViewModel> Handle(CheckConnectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var client = new SSHClient(request.Credentials))
                {
                    return new CheckConnectionViewModel() { IsAlive = client.CheckConnection() };
                }
            }
            catch (Exception c)
            {
                return new CheckConnectionViewModel() { IsError = true, Error = c.Message, IsAlive = false };
            }
        }
    }
}
