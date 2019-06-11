using Microsoft.Extensions.Caching.Memory;
using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace SSH.Application.Base
{
    public class BaseCommandHandler
    {
        protected IMemoryCache _cache;
        public BaseCommandHandler(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        protected bool Ping(Credentials credentials)
        {
            bool alive = false;
            if (!_cache.TryGetValue(credentials.Hostname, out alive))
            {
                var ping = new Ping();
                PingReply pingresult = ping.Send(credentials.Hostname);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                      .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                if (pingresult.Status.ToString() == "Success")
                {
                    alive = true;
                    _cache.Set(credentials.Hostname, true, cacheEntryOptions);
                }
                else
                {
                    alive = false;
                    _cache.Set(credentials.Hostname, false, cacheEntryOptions);
                }
            }
            return alive;
        }
    }
}
