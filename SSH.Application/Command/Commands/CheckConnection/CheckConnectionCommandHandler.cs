using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SSH.Application.Base;
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
        private IMemoryCache _cache;
        public CheckConnectionCommandHandler(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<CheckConnectionViewModel> Handle(CheckConnectionCommand request, CancellationToken cancellationToken)
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
                    _cache.Set(request.Credentials.Hostname, true, cacheEntryOptions);
                    return new CheckConnectionViewModel() { IsAlive = true };
                }
                else
                {
                    _cache.Set(request.Credentials.Hostname, false, cacheEntryOptions);
                    return new CheckConnectionViewModel() { IsError = true, IsAlive = false, Error = "Couldnt connect to server" };
                }
            }
            return new CheckConnectionViewModel()
            {
                IsAlive = alive,
                IsError = !alive,
                Error = alive ? "" : "Couldnt connect to server"
            };
        }
    }
}
