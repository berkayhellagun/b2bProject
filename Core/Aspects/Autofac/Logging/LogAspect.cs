using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.NLog;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerBase _loggerBase;

        public LogAspect(Type logger)
        {
            if (logger.BaseType != typeof(LoggerBase))
            {
                throw new Exception("Invalid logger type.");
            }
            _loggerBase = (LoggerBase)Activator.CreateInstance(logger);
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _loggerBase.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameter = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameter.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = (string)invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            var logDetail = new LogDetail
            {
                LogParameter = logParameter,
                MethodName = invocation.Method.Name
            };

            return logDetail;
        }
    }
}
