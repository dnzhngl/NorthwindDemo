using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolvers
{
    // Bütün projelerde kullanacağımız IoC injectionları burada bulunur.
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache(); // dotnet yapısı olarak, otomatik olarak IMemoryCache injectioni yapar.Yani arka planda CacheManager instanceı oluşturuyor.
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>(); // Bizim oluşturmuş olduğumuz ICacheManagerın karşılığı olarak microsoftun memory cache managerını ver diyoruz.
            // serviceCollection.AddSingleton<ICacheManager, RedisCacheManager>(); // Cache yapımızı redis ile değiştirmek istersek, RedisCache eklemek için tek yapman gereken buradaki MemoryCache'i RedisCache ile değiştirmek.
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
