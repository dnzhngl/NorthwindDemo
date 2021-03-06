using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        // içerisine IResult tipinde array olarak parametre alıyor. İş kurallarının dizi halinde gönderip çalışmasını sağlar.
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success) // logic'in success durumu başarısız ise;
                {
                    return logic;   // başarısız olan logici business'a geri gönder.
                }
            }
            return null; // başarılı ise bişey döndürmesine gerek yok o yüzden null döndür dedik.
        }


        #region Burada bütün hataların tek seferde dönmesini sağlayabiliriz.
        //public static List<IResult> Run(params IResult[] logics)
        //{
        //    List<IResult> errorResults = new List<IResult>();
        //    foreach (var logic in logics)
        //    {
        //        if (!logic.Success) 
        //        {
        //            errorResults.Add(logic);
        //        }
        //    }
        //    return errorResults; 
        //}
        #endregion
    }
}
