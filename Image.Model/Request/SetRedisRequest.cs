using System;
using System.Collections.Generic;
using System.Text;

namespace Image.Model.Request
{
    public class SetRedisRequest
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
