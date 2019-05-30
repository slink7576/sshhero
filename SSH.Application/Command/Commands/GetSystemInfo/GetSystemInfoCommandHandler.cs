using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace SSH.Application.System.Commands.GetSystemInfo
{
    public class GetSystemInfoCommandHandler : IRequestHandler<GetSystemInfoCommand, SystemInfoViewModel>
    {
        public async Task<SystemInfoViewModel> Handle(GetSystemInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ping = new Ping();
                PingReply pingresult = ping.Send(request.Credentials.Hostname);
                if (pingresult.Status.ToString() == "Success")
                {
                    using (var client = new SSHClient(request.Credentials))
                    {
                        return new SystemInfoViewModel() { OS = client.GetInfo().Os };
                    }
                }
                else
                {
                    return new SystemInfoViewModel() { IsError = true, OS = null };
                }
               
            }
            catch(Exception c)
            {
                return new SystemInfoViewModel() { Error = c.Message, IsError = true, OS = null };
            }
            
        }
    }
}
