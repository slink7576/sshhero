using MediatR;
using Microsoft.Extensions.Caching.Memory;
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
        private IMemoryCache _cache;
        public RebootCommandHandler(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<bool> Handle(RebootCommand request, CancellationToken cancellationToken)
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
                    alive = true;
                    _cache.Set(request.Credentials.Hostname, true, cacheEntryOptions);
                }
                else
                {
                    alive = false;
                    _cache.Set(request.Credentials.Hostname, false, cacheEntryOptions);
                }
            }
            if (alive)
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
    }
}
