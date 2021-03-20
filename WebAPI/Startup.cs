﻿using Business.Abstract;
using Business.Concrete;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            #region Before Autofac

            // Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container 
            // Autofac kullanmayı tercih etme sebebimiz bize AOP imkanı sunduğundan. 

            // Burası ise Built in IoC container
            // - singleton - tüm bellekte bir tane instance oluşturuyor kim isterse istesin ona aynı referansı veriyor. içerisinde data tutmadığınız durumlarda kullanılır. 
            // -AddScoped , AddTrensient  = içerisinde data tutulan durumlarda kullanılır.

            //services.AddSingleton<IProductService, ProductManager>(); // birisi constructorda senden IProductService isterse sen ona newlenmiş ProductManager ver.
            //services.AddSingleton<IProductDal, EfProductDal>();  

            // Autofac yapılandırmasını PRogram.cs içerisinde yapıyoruz.
            #endregion
            #region Before CoreModule implemetation

            // Client tarafından her yapılan istek ile ilgili oluşan context. İsteğin oluştuğu andan yanıtın döndüğü zaman kadar olan herşeyi HttpContextAccessor kontrol ediyor. Buradaki injection tüm projelerde kullanılacak olan bir yapı. O yüzden burası Core'a gider.

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion

            // Cors Policy hatası: backendde apiye erişilmesi için gerekli configurasyonların yapılmadığını gösterir.
            // CORS : Cross Origin Request : farklı bir yerden, originden istek gelmesi
            // Bizim uygulamamızın çalıştığından farklı bir adresten istek gelmesi durumudur. Günümüzdeki popüler tüm tarayıcılarda olan bir güvenlik mekanizmasıdır. Burada amaç, tarayıcı oraya herhangi bir xss dediğimiz bir atak ile oraya eklenmiş birşey olmadığından emin olmak istiyor. O yüzden, geliştiriciye soruyor, bu adrese güveniyor musun?
            services.AddCors(); // Cors Injection yapıyoruz.

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            // İhtiyaç duyduğum her modulü buraya ekleyebileyim.
            services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule() //, new DifferentModule()
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//middleware -> asp.net yaşam döngüsünde hangi yapıların sırası ile devreye gireceğini söylüyoruz. ihtiyacın olanı buraya yazıp devreye sookuyorsunz.
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.ConfigureCustomExceptionMiddleware(); // Custom olarak yazmış olduğumuz middleware. Exceptionları handllelar.  Bu hareket ile bütün sistemi api kurallarına uyarak try catch içerisine almış olduk.

            // UseCors'un yazıldığı yer önemli.
            app.UseCors(builder => builder.WithOrigins().AllowAnyHeader().AllowAnyOrigin()); // Cors yapılandırması için nereye/hangi adrese izin verdiğimizi belirtiyoruz. Buradaki urlden gelen sorgulara izin ver anlamına geliyor. AllowAnyHeader() : Ne isteği yaptığı önemli değil,(get, post, put vb.) bu adrese güveniyoruz, her konuda izin veriyoruz.
            // Birden fazla url erişimine izin vermek istiyorsan, WithOrigins içerisine virgül ile ayırarak hepsini girebilirsin.
            //app.UseCors(builder => builder.WithOrigins("http://localhost:55183").AllowAnyHeader()); 

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // login & registration vb.

            app.UseAuthorization();  // roles 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
