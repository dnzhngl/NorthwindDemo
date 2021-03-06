using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    // Metodun performansı ile ilgilenir. Sistem yavaşlıyor vb. sebeplerin kaynağını bulmaya yardımcı olur.
    // Özellikle yoğun sorgulama operasyonlarında sistemde bir performans zaafiyeti varsa sistem bizi uyarsın.
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;
        // Kullanılırken içerisine zaman aralığı verilmek zorundadır.
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        // Metodtan önce kronometre başlatır.
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        // Metod sonunda toplam geçen saniye, verilen zaman aralığından fazla ise console'a bilgileri yazar.
        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset();
        }
    }
}
