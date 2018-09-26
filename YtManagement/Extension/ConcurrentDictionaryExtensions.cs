using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YtManagement.Extension
{
    internal static class ConcurrentDictionaryExtensions
    {
        public static void AddUpdateOrRemove<K,V>(this ConcurrentDictionary<K,V> source, IEnumerable<V> updatedList, Func<V,K> keySelector)
        {
            foreach (var item in updatedList)
            {
                source.AddOrUpdate(keySelector(item), item, (key, oldValue) => item);
            }

            var removeKeys = source.Where(o => !updatedList.Any(p => keySelector(p).Equals(o.Key))).Select(o => o.Key).ToList();
            foreach (var item in removeKeys)
            {
                source.TryRemove(item, out _);
            }
        }
    }
}
