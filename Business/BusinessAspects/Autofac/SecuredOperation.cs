using Business.Constants;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;

namespace Business.BusinessAspects.Autofac
{
    // JWT için
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; // JWT yi de göndererek yapmış olduğumuz request için bir httpContext oluşur. Her bir kişi için ayrı ayrı oluşur. Bu Acce

        public SecuredOperation(string roles) // constructorda rolleri alır. roller virgülle ayrılarak geliyor.
        {
            _roles = roles.Split(','); // virgülle ayrılarak gelen rolleri virgüle göre ayırıp arraye atıyor. (_roles arrayi)
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
           // Örn: Diğer UI katmanlarında -->  productService = ServiceTool.ServiceProvider.GetService<IProductService>(); --> Şeklinde autpfacteki ilgili servisi alıp service'i oluşturabilir ve kullanabiliriz. 
        }

        protected override void OnBefore(IInvocation invocation)
        {
            // o an ki kullanıcının rollerini alır.
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)    // kullanıcının rolleri arasında gez
            {
                if (roleClaims.Contains(role)) //  ilgili rol var ise dön, ilgili metod çalışsın(invocation)
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied); // yoksa AuthorizationDenied Hatası döndür.
        }
    }
}
