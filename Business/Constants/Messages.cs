using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    // sürekli newlememek için static verdik. Uygulama hayatı boyunca tek instanceı oluyor.
    public static class Messages
    {
        public static string MaintenanceTime = "Sistem bakımda.";
        public static string Error = "Bir hata oluştu.";
        public static string NotFound = "Hiç bir kayıt bulunamadı";

        public static class Products
        {
            public static string Add(string productName)
            {
                return $"{productName} isimli ürün sisteme başarı ile eklenmiştir.";
            }
            public static string Update(string productName)
            {
                return $"{productName} isimli ürün bilgileri başarılı bir şekilde güncellenmiştir.";
            }
            public static string Delete(string productName)
            {
                return $"{productName} isimli ürün sistemden başarılı bir şekilde silinmiştir.";
            }
            public static string Exists(string productName)
            {
                return $"{productName} bilgilerine sahip bir ürün sistemde kayıtlıdır.";
            }
        }
        public static class Categories
        {
            public static string Add(string categoryName)
            {
                return $"{categoryName} isimli kategori sisteme başarı ile eklenmiştir.";
            }
            public static string Update(string categoryName)
            {
                return $"{categoryName} isimli kategori bilgileri başarılı bir şekilde güncellenmiştir.";
            }
            public static string Delete(string categoryName)
            {
                return $"{categoryName} isimli kategori sistemden başarılı bir şekilde silinmiştir.";
            }
            public static string Exists(string categoryName)
            {
                return $"{categoryName} bilgilerine sahip bir kategori sistemde kayıtlıdır.";
            }
        }
        public static class Customers
        {
            public static string Add(string companyName, string city)
            {
                return $"{companyName} - {city} bilgilerine sahip müşteri sisteme başarı ile eklenmiştir.";
            }
            public static string Update(string companyName, string city)
            {
                return $"{companyName} - {city} müşteri bilgileri başarılı bir şekilde güncellenmiştir.";
            }
            public static string Delete(string companyName, string city)
            {
                return $"{companyName} - {city} müşteri sistemden başarılı bir şekilde silinmiştir.";
            }
            public static string Exists(string companyName, string city)
            {
                return $"{companyName} - {city} bilgilerine sahip bir müşteri sistemde kayıtlıdır.";
            }
        }
        public static class Orders
        {
            public static string Add()
            {
                return $"Siparişiniz işleme alınmıştır.";
            }
            public static string Update()
            {
                return $"Sipariş bilgileriniz başarılı bir şekilde güncellenmiştir.";
            }
            public static string Delete()
            {
                return $"Siparişiniz sistemden başarılı bir şekilde silinmiştir.";
            }
        }


    }
}
