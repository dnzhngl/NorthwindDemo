using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    // Hashleme aracı
    public class HashingHelper 
    {
        // verilen passwordun hashini ve saltını oluşturacak yapıyı içeriyor. // out keywordu dışarıya veri döndürmesini sağlayacak
        // dotnet'in kriptografi sınıflarından yararlanarak şifrelerimiz oluşturacağız. Disposable pattern ile oluşturacağız.
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)  
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) // HMAC - MD5 -şifreleme algoritmaları classları
            {
                passwordSalt = hmac.Key; // Key --> kullanılan algoritmanın o an kullanan kullanıcı oluşturduğu bir ke değeridir.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Encoding.UTF8.GetBytes(password) --> string'in byte karşılığını verir.
            }
        }

        // Gelen passwordü, aynı algoritmayı kullanarak hashle, veritabanındaki passwordhash ile karşılaştır. İkisi birbine eşitse true döner. 
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) // hmac olarak hesaplanan hash kullanıcnın databasedeki passwordSalt değeri kullanılarak oluşturuluyor.
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // girilen passworrdun hashını oluşturur.
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i]) // hesapladğımız hash arrayı ile databaseden gelen hash arayının değerleri karşılaştırılır. eğer aynı olmadığı bir durum olursa false döner.
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
