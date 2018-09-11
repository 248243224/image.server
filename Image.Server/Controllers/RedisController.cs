using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Image.Common.Cache;
using Image.Common.Log;
using Image.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace Image.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RedisController : Controller
    {
        private readonly ILog _logger;
        private ICache _cache;
        public RedisController(ILog logger, ICache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]
        public string Set(SetRedisRequest request)
        {
            var retMsg = "";
            _cache.Set(request.Key, request.Value);
            retMsg = $"set redis key: {request.Key} value: {request.Value} success";
            return retMsg;
        }
        [HttpGet]
        public string Get(string key)
        {
            var retMsg = "";
            var value = _cache.Get<string>(key);
            retMsg = $"get redis key: {key} value :{value} success";
            return retMsg;
        }

    }
}
