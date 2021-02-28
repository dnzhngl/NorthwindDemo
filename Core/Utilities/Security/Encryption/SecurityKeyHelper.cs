using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    // İşin içeriisnde şifreleme olan sistemlerde herşeyi byte[] formatında oluşturmamız gerekiyor. 
    // basit bir string ile key oluşturamazsınız. 
    public class SecurityKeyHelper
    {
        // appsettings.jsondaki SecurityKey parametre oalrak gelir.
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)); // 
        }
    }
}
