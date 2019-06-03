using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
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
    }
}
