using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    /// <summary>
    /// Hata olması durumunda, apinin nasıl davranması gerektiğini gösterir.
    /// </summary>
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Bütün kodları try catch içerisine alır. Hata oluşursa handlelar, oluşmazsa normal çalışır.
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json"; // Tarayıcıya gönderilecek olan verinin tipini json olarak veriyoruz.
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;  

            string message = "Internal Server Error";
            // Hatanın türü doğrulama hatası ise;
            IEnumerable<ValidationFailure> errors; // Fluentvalidation'dan kaynaklı hata verinde ValidationFailure türünde hata verir.
            if (e.GetType() == typeof(ValidationException)) // Eğer alınan hata validation exception hatası ile 
            {
                message = e.Message;    // igili validationın mesajını döner.
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = 400; // hata validation hatası ise kodun statusunu 400 yani bad request hatasını verdirdik.

                return httpContext.Response.WriteAsync(new ValidationErrorDetails // Validation hatası aldığımız durumda validation errorlarınıda döndürdüğümüz bir validation error details nesnemiz var.
                {
                    StatusCode = 400,
                    Message = message,
                    ValidationErrors = errors
                }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorDetails // Sistemsel hatalarda, yani sistem hata verirse bunu döner.
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
