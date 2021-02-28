using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    // Helper class -> Proje içeriisnde yardımcı olması için oluşturulmuş olan bir class. O yüzden çoğul. 
    // Api/appsetting.json daki TokenOptions configurasyonunu simgeler.
    public class TokenOptions 
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
