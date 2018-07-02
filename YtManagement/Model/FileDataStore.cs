using Google.Apis.Util.Store;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using YtManagement.Common.Model;
using YtManagement.Storage;

namespace YtManagement.Model
{
    public class FileDataStore : IDataStore
    {
        private readonly ConcurrentDictionary<string, TokenContainer> _cache = new ConcurrentDictionary<string, TokenContainer>();
        private readonly IStorage _storage;

        public FileDataStore(IStorage storage)
        {
            this._storage = storage;
            var containerResult = this._storage.Load<TokenContainer>();
            if(containerResult.Status != ActionStatus.Success)
            {
                throw new TypeLoadException(containerResult.Message);
            }
            foreach (var item in containerResult.Data)
            {
                this._cache.TryAdd(item.Key, item);
            }
        }
        public Task ClearAsync()
        {
            this._cache.Clear();
            this._storage.Save(this._cache);
            return Task.FromResult(true);
        }

        public Task DeleteAsync<T>(string key)
        {
            this._cache.TryRemove(key, out var trash);
            this._storage.Save(this._cache);
            return Task.FromResult(true);
        }

        public Task<T> GetAsync<T>(string key)
        {
            if(!this._cache.TryGetValue(key, out var item))
            {
                return Task.FromResult(default(T));
            }
            return Task.FromResult(((JObject)item.Data).ToObject<T>());
        }

        public Task StoreAsync<T>(string key, T value)
        {
            this._cache.TryAdd(key, new TokenContainer { Key = key, Data = value });
            this._storage.Save(this._cache);
            return Task.FromResult(true);
        }
    }
}
