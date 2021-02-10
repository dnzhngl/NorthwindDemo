using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    // sürekli newlememek için static verdik. Uygulama hayatı boyunca tek instanceı oluyor.
    public static class Messages
    {
        public static string ProductAdded = "Ürün başarı ile eklendi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz.";
        public static string MaintenanceTime = "Sistem bakımda.";
        public static string ProductsListed = "Ürünler listelendi.";
    }
}
