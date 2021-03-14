using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    // Service collection için extension 
    public static class ServiceCollectionExtensions
    {
        // Asp.Net uygulamamızın yani apimizin servis bağımlılıklarını eklediğimiz yada araya girmesini istediğimiz servisleri eklediğimiz koleksiyonun kendisidir.
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection); //modulü yükle
            }
            return ServiceTool.Create(serviceCollection); // ilgili service collection'ı yarat.
        }
    }
}
