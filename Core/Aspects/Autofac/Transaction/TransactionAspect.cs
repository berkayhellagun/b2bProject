using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transaction.Complete();
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
    }
}
