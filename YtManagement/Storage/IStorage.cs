using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YtManagement.Model;

namespace YtManagement.Storage
{
    public interface IStorage
    {
        ActionResult<List<T>> Load<T>();
        ActionResult Save<T,V>(ConcurrentDictionary<T,V> cache);
    }
}
