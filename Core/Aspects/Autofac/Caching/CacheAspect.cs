using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        /// <summary>
        /// Constructor da, duration/süre verilmediyse default olarak 60 dakika atanır. 
        /// ServiceTool ile ICacheManager injekte edilir. 
        /// Yani, kullanılan cache teknolojisine ICacheManager interfacei uygulandığı sürece burada herhangi bir değişiklik yapmaya gerek yok.
        /// </summary>
        /// <param name="duration"></param>
        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); //GetService için -> Microsoft.Extensions.DependencyInjection;
        }

        /// <summary>
        /// </summary>
        /// <param name="invocation">Methodumuzun kendisini temsil eder.</param>
        public override void Intercept(IInvocation invocation)
        {
            // invocation : method , örn: GetAll
            // reflected type : namespace + bulunduğu class/interface(biz interface üzerinden çalıştığımız için.)
            // methodun yolunu verir. Örn: Business.Abstract.IProductService gibi.
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); 

            var arguments = invocation.Arguments.ToList(); // Arguments -> İlgili metodun parametrelirini temsil eder. listeler.

            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; //Business.Abstract.IProductService.GetAll() //Business.Abstract.IProductService.GetById(1)
            if (_cacheManager.IsAdd(key)) // cachede var mı yok, yoksa atlar varsa içerisine girer. cache'deki veriyi döndürür.
            {
                invocation.ReturnValue = _cacheManager.Get(key); // metodu çalıştırmadan dönüş değeri olarak diğer, cachedeki veriyi atar. 
                return;
            }
            invocation.Proceed(); // cachede yoksa metodu çalıştırır. metodunu çalıştırır. metod veritabanından döndükten sonra buradan devam eder.
            _cacheManager.Add(key, invocation.ReturnValue, _duration); // metoddan dönen veriyi cache 60 dklığına ekler.
        }
    }
}
