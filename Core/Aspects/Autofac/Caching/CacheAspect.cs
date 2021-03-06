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
        /// Constructor da durtion verilmediyse default olarak 60 dakika atanır. ServiceTool ile ICacheManager injekte edilir.
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
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); // methodun yolunu verir. Örn: Business.Abstract.IProductService gibi.
            var arguments = invocation.Arguments.ToList(); // Arguments -> İlgili metodun parametrelirini temsil eder. listeler.
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; //Business.Abstract.IProductService.GetAll() //Business.Abstract.IProductService.GetById(1)
            if (_cacheManager.IsAdd(key)) // cachede var mı yok, yoksa atlar varsa içerisine girer. cache'deki veriyi döndürür.
            {
                invocation.ReturnValue = _cacheManager.Get(key); // metodu çalıştırmadan dönüş değeri olarak diğer cachedeki veriyi atar. 
                return;
            }
            invocation.Proceed(); // cachede yoksa metodu çalıştırır. 
            _cacheManager.Add(key, invocation.ReturnValue, _duration); // metoddan dönen veriyi cache 60 dklığına ekler.
        }
    }
}
