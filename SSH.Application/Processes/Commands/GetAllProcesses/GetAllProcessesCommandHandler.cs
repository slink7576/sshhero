using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSH.Application.Processes.Query.GetAllProcesses
{
    public class GetAllProcessesCommandHandler : IRequestHandler<GetAllProcessesCommand, ProcessesListViewModel>
    {
        public async Task<ProcessesListViewModel> Handle(GetAllProcessesCommand request, CancellationToken cancellationToken)
        {
            using(var client = new SSHClient(request.Credentials))
            {
                return new ProcessesListViewModel() { Processes = client.GetProcesses() };
            }
        }
    }
}
