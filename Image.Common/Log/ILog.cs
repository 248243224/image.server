using System;
using System.Collections.Generic;
using System.Text;

namespace Image.Common.Log
{
    public interface ILog
    {
        void Info(string message);
        void Error(string message);

        void Warning(string message);
    }
}
