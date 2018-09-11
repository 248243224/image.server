using System;
using System.Collections.Generic;
using System.Text;
using Image.Common.Cache;
using ServiceStack.Redis;

namespace Image.Common.Redis
{
    public class RedisClient : ICache
    {
        private RedisManagerPool _redisManagerPool;
        public RedisClient()
        {
            _redisManagerPool = new RedisManagerPool("dev.handsave.com:8002");
        }

        public void Set(string key, object value)
        {
            using (var client = _redisManagerPool.GetClient())
            {
                client.Set(key, value);
            }
        }
        public T Get<T>(string key)
        {
            using (var client = _redisManagerPool.GetClient())
            {
                return client.Get<T>(key);
            }
        }
    }
}
