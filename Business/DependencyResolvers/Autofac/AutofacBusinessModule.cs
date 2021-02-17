using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    // Bu projeyi ilgilendiren configurasyonu burada yaptığımız için business module adını verdik.
    public class AutofacBusinessModule : Module //Autofac Module'unden implement edildi. Startuptaki dependency injection ortamını kurmamızı sağlıyor.
    {
        protected override void Load(ContainerBuilder builder) // Uygulama hayata geçtiği zaman, bu kod bloğu çalışacak.
        {
            // Singleınstance => AddSıngleton  : bir instance üretir herkese onu verir.
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();  // Senden IProductService istendiğinden ProductManager'ı register et. (ProductManager instance'ı ver.)
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
