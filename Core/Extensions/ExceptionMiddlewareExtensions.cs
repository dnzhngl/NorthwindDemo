using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    /// <summary>
    /// Custom middleware
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        // Web apinin startupında sisteme ekledik.
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>(); // projeye ayağa kalktığında çalışmasını istediğimiz kod. Bizim oluşturmuş olduğumuz exception middleware
        }
    }
}
