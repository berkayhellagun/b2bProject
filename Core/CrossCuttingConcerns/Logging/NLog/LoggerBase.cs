using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core.CrossCuttingConcerns.Logging.NLog
{
    public class LoggerBase
    {
        private readonly ILogger _log;

        public LoggerBase(string type)
        {
            _log = LogManager.GetLogger(type);
        }
        public bool IsInfoEnabled => _log.IsInfoEnabled;
        public bool IsDebugEnabled => _log.IsDebugEnabled;
        public bool IsWarnEnabled => _log.IsWarnEnabled;
        public bool IsFatalEnabled => _log.IsFatalEnabled;
        public bool IsErrorEnabled => _log.IsErrorEnabled;

        public void Info(object Message)
        {
            if (IsDebugEnabled)
                _log.Info(Message);
        }

        public void Debug(object Message)
        {
            if (IsDebugEnabled)
                _log.Debug(Message);
        }

        public void Warn(object Message)
        {
            if (IsDebugEnabled)
                _log.Warn(Message);
        }

        public void Fatal(object Message)
        {
            if (IsDebugEnabled)
                _log.Fatal(Message);
        }

        public void Error(object Message)
        {
            if (IsDebugEnabled)
                _log.Error(Message);
        }
    }
}
