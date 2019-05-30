using MediatR;
using Microsoft.Extensions.Caching.Memory;
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
        private IMemoryCache _cache;
        public GetAllProcessesCommandHandler(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<ProcessesListViewModel> Handle(GetAllProcessesCommand request, CancellationToken cancellationToken)
        {
            bool alive = false;
            if (!_cache.TryGetValue(request.Credentials.Hostname, out alive))
            {
                var ping = new Ping();
                PingReply pingresult = ping.Send(request.Credentials.Hostname);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                      .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                if (pingresult.Status.ToString() == "Success")
                {
                    using (var client = new SSHClient(request.Credentials))
                    {
                        _cache.Set(request.Credentials.Hostname, true, cacheEntryOptions);
                        return new ProcessesListViewModel() { Processes = client.GetProcesses() };
                    }
                }
                else
                {
                    _cache.Set(request.Credentials.Hostname, false, cacheEntryOptions);
                    return new ProcessesListViewModel()
                    {
                        IsError = true,
                        Processes = null,
                        Error = "Couldnt connect to server"
                    };
                }
            }
            return new ProcessesListViewModel()
            {
                IsError = !alive,
                Processes = null,
                Error = alive ? "" : "Couldnt connect to server"
            };
        }
    }
}
