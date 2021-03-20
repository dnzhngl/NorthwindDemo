using Newtonsoft.Json;
using System;
using System.Text;

namespace Core.Extensions
{
    // Sistemsel hatalarda kullanılması beklenen hata sınıfı. Mesaj ve hatanın status kodunu içerir.
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
