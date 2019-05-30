using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
                var ping = new Ping();
                PingReply pingresult = ping.Send(request.Credentials.Hostname);
                if (pingresult.Status.ToString() == "Success")
                {
                    return new CheckConnectionViewModel() { IsAlive = true };
                }
                else
                {
                    return new CheckConnectionViewModel() { IsError = true, IsAlive = false };
                }
            }
            catch (Exception c)
            {
                return new CheckConnectionViewModel() { IsError = true, Error = c.Message, IsAlive = false };
            }
        }
    }
}
