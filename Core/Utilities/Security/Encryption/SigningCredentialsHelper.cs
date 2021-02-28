using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        // JWT servicelerinin - web apinin kullnabileceği- gelen web tokenı doğrulaması için buradak hang anahtarı ve hangi algoritmayı kullanacağını veriyoruz.
        //appsettings.json daki securiy key'i ve şifreleme algoritmasını kullanıcı bilgilerin bunlar diye döndürüyoruz.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature); //HmacSha512Signature diğerleri gibi algoritma versiyonları, güçlendirilmiş halleri.
        }
    }
}
