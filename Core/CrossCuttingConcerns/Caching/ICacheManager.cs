using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    /// <summary>
    /// Bütün altertif Cache yapıları için kullanılabilecek genel bir Cacheleme yapısını içerir.
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Generic Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        /// Generic olmayan Get metodu
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);
        /// <summary>
        /// Cachlenecek yapıyı ekler.
        /// </summary>
        /// <param name="key">Cache verilen isim.</param>
        /// <param name="value">Eklenecek olan nesne.</param>
        /// <param name="duration">Cachcede kalacağı süre.</param>
        void Add(string key, object value, int duration);
        /// <summary>
        /// Bellekte böyle bir cache değeri var mı? 
        /// Cache de var mı kontrolünü sağlar.
        /// </summary>
        /// <returns></returns>
        bool IsAdd(string key);
        /// <summary>
        /// Cache'den silme
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
        /// <summary>
        /// Verilen Regex desenine sahip olanları siler.
        /// Örn: adında Get olanları sil gibi. Adının içerisinde get geçmesi yeterli.
        /// </summary>
        void RemoveByPattern(string pattern);
    }
}
