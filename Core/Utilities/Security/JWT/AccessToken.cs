using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken // Erişim anahtarı classı
    {
        public string Token { get; set; } // jeton numarası gibi düşün
        public DateTime Expiration { get; set; } // Bitiş süresi, tokenin geçerliliğinin bitiş tarihi, jetonun son kullanma tarihi
    }
}
