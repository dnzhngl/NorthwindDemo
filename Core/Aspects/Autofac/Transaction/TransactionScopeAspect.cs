using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    /// <summary>
    /// Çalıştırılmak istenen operasyon gerçekleşirken hata oluşursa yapılmış olan işlemler geri alınır.
    /// Bu işlemler veri tabanı belleğinde olur.
    /// Bir hata oluşursa, transaction henüz bitmediği için yapılan işlemler geri alınır.
    /// </summary>
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
