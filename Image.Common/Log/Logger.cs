using System;

namespace Image.Common.Log
{
    public class Logger : ILog
    {
        public void Info(string message)
        {
            NLog.LogManager.GetLogger("info").Info(message);
        }

        public void Error(string message)
        {
            NLog.LogManager.GetLogger("error").Error(message);
        }
        public void Warning(string message)
        {
            NLog.LogManager.GetLogger("warning").Error(message);
        }
        private void Debug(string message)
        {
            NLog.LogManager.GetLogger("debug").Info(message);
        }
    }
}
