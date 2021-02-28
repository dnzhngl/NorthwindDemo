using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }    // Api/appsettings.json içerisindeki değerleri okumamızı sağlıyor.
        private TokenOptions _tokenOptions;             // Api/appsetting.json içerisinde yazmış olduğumuz TokenOptions'a ulaşmamızı sağlar.
        private DateTime _accessTokenExpiration;        // AccessToken ne zaman geçersizleşecek.
        public JwtHelper(IConfiguration configuration) // IConfiguration'ı constructor'a enjekte ederek JwtHelper'in appsettings.json'a erişmesini sağlıyoruz.
        {
            Configuration = configuration;
            _tokenOptions =  Configuration.GetSection("TokenOptions").Get<TokenOptions>();  // appsetting içerisindeki TokenOptions bölümünü/sectionini al, TokenOptions classındaki değerlere maple.
            // Configurationa bağlı olan GetSection() -> appsettings.jasondaki her bir sekmeyi alanı temsil eder.
            // Get<T> --> verilen T ile maple.
        }

        // ITokenHelper'dan implement eder.
        // Token oluşturmak için kullanıcı bilgilerini ve OperationClaims yani hangi operasyonları yapmaya yetkisi olduğunu söyleyen listeyi alır.
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); // TokenOptions'dan Access token expiration'dan  bitiş tarihini çeker

            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);  // TokenOptions'daki security key'i alır,  SecurityKeyHelper ile security key oluşturur.
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);    // Giriş yapma bilgileri
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims); // Security Token oluşturur.
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        // JWT security token oluşturur.
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,  // şuandan önceki bir değer verilemez.
                claims: SetClaims(user, operationClaims),   // kullanıcın claimlerini oluşturur.
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        // Kullanıcının Claimlerini oluşturur.
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray()); // operasyonlar bazında yetkilerini rollerine ekler.

            return claims;
        }
    }
}
