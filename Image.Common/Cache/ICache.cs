using System;
using System.Collections.Generic;
using System.Text;

namespace Image.Common.Cache
{
    public interface ICache
    {
        void Set(string key, object value);
        T Get<T>(string key);
    }
}
