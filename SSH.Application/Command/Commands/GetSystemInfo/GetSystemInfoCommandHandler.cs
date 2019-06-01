﻿using MediatR;
using SSH.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Microsoft.Extensions.Caching.Memory;

namespace SSH.Application.System.Commands.GetSystemInfo
{
    public class GetSystemInfoCommandHandler : IRequestHandler<GetSystemInfoCommand, SystemInfoViewModel>
    {
        private IMemoryCache _cache;
        public GetSystemInfoCommandHandler(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<SystemInfoViewModel> Handle(GetSystemInfoCommand request, CancellationToken cancellationToken)
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
                    return new SystemInfoViewModel() { OS = client.GetInfo().Os };
                }
            }
            else
            {
                return new SystemInfoViewModel()
                {
                    IsError = true,
                    OS = null,
                    Error = "Couldnt connect to server"
                };
            }
        }
    }
}
