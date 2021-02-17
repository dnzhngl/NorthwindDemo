using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            // Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container 
            // Autofac kullanmayı tercih etme sebebimiz bize AOP imkanı sunduğundan. 

            // Burası ise Built in IoC container
            // - singleton - tüm bellekte bir tane instance oluşturuyor kim isterse istesin ona aynı referansı veriyor. içerisinde data tutmadığınız durumlarda kullanılır. 
            // -AddScoped , AddTrensient  = içerisinde data tutulan durumlarda kullanılır.
            #region Before Autofac
            //services.AddSingleton<IProductService, ProductManager>(); // birisi constructorda senden IProductService isterse sen ona newlenmiş ProductManager ver.
            //services.AddSingleton<IProductDal, EfProductDal>();  

            // Autofac yapılandırmasını PRogram.cs içerisinde yapıyoruz.
            #endregion


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
