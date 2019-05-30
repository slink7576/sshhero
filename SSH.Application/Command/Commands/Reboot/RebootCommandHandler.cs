using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSH.Application.Command.Commands.Reboot
{
    public class RebootCommandHandler : IRequestHandler<RebootCommand, bool>
    {
        public async Task<bool> Handle(RebootCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ping = new Ping();
                PingReply pingresult = ping.Send(request.Credentials.Hostname);
                if (pingresult.Status.ToString() == "Success")
                {
                    using (var client = new SSHClient(request.Credentials))
                    {
                        return client.Reboot();
                    }
                }
                else
                {
                    return false;
                }               
            }
            catch(Exception c)
            {
                return false;
            }
        }
    }
}
