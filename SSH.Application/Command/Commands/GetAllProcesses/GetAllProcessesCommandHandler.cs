using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSH.Application.Processes.Query.GetAllProcesses
{
    public class GetAllProcessesCommandHandler : IRequestHandler<GetAllProcessesCommand, ProcessesListViewModel>
    {
        public async Task<ProcessesListViewModel> Handle(GetAllProcessesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ping = new Ping();
                PingReply pingresult = ping.Send(request.Credentials.Hostname);
                if (pingresult.Status.ToString() == "Success")
                {
                    using (var client = new SSHClient(request.Credentials))
                    {
                        return new ProcessesListViewModel() { Processes = client.GetProcesses() };
                    }
                }
                else
                {
                    return new ProcessesListViewModel() { IsError = true, Processes = null };
                }
               
            }
            catch(Exception c)
            {
                return new ProcessesListViewModel() { Error = c.Message, IsError = true, Processes = null };
            }
        }
    }
}
