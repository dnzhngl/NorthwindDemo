using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        // Result classı newlendiği zaman parametre olarak success ve message bilgileri girilir.
        public Result(bool success, string message) : this (success)// buradaki success'i this(bu classtaki yani Result classında) ki tek paramtereli olanı çalıştırır ve ona success'i gönder. 
            // Eğer hem success hemde message birlikte gönderilirse, iki constructorda çalışır. message'i ilkinde set eder, success'i ilk construcotrdan ikinci constructora gönderir ve orada set eder. 
        {
            Message = message;
        }
        public Result(bool success) // Constructor overloading
        {
            Success = success;
        }

        public bool Success { get; }    // Getter readonly'dir. Getter'lar constructorda set edilebilir.
        public string Message { get; }
        public int Count { get; set; }
    }
}
