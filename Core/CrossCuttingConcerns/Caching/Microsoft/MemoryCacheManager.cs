using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        // Çözümlemesini CoreModule içerisinde gerçekleştirdik.
        // Adapter pattern : Adaptasyon deseni -> Var olan bir sistemi kendi sistemime uyarlıyorum. Ben sana göre çalışmayacağım. Sen benim sistmeime göre çalışacaksın.
        IMemoryCache _memoryCache;
        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>(); // Microsoft.Extensions.DependencyInjection gerekiyor.
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration)); // verilen durationa göre, o kadar süre içinde cachede tutulur.
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _); // out _ : discard -> out olarak herhangi birşey döndürmesini istemiyorum
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        /// <summary>
        /// Çalışma anında bellekten silmeye yarar.
        /// Çalışma anında bellekte bulunan bir sınıfın instance'ı üzerinde işlem yapıyoruz. -> Bunun için reflection kullanıyoruz. 
        /// Öcelikle bellekte cache ile ilgili olan yapıyı çekmek istiyorum.
        /// </summary>
        /// <param name="pattern"></param>
        public void RemoveByPattern(string pattern)
        {
            // Git belleğe bak, bellekte MemoryCache türünde olan, adı EntriesCollection olan  ve non-public yada instance olan propertyleri bul.
            // EntriesCollection -> microsoft, cachlediğinde cache datalarını EntriesCollection içerisine attığını dokümantasyonda veriyor.
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            //Bulduğun propertyler içerisinde definitionı memoryCache olanların valuesını al.
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;

            // ICache tipinde liste oluştur.
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            // cacheEntriesCollection içindeki her bir item için
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null); // cache elemanının value'sunu al.
                cacheCollectionValues.Add(cacheItemValue); // cacheCollectionValues'a ekle.
            }

            // regex kuralımız -> patternımız single line olacak, compiled olacak ve case sensitive olmayacak.
            // gönderilen patern uygulanacak.
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            // cache collection içerisinde verdiğimiz regex patternine sahip olan keyleri bul listele.
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            // silinecek keylerin listesini gez hepsini tek tek sil.
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
