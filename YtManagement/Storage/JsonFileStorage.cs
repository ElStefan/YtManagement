using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using YtManagement.Common.Model;

namespace YtManagement.Storage
{
    public class JsonFileStorage : IStorage
    {
        public ActionResult<List<T>> Load<T>()
        {
            var targetFile = GetFilePath<T>();
            try
            {
                if(!File.Exists(targetFile))
                {
                    return new ActionResult<List<T>>(ActionStatus.Success, new List<T>());
                }
                var content = File.ReadAllText(targetFile);
                var list = JsonConvert.DeserializeObject<List<T>>(content);
                return new ActionResult<List<T>>(ActionStatus.Success, list);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return new ActionResult<List<T>>(ActionStatus.Error, $"Exception: {exception.Message}");
            }
        }

        private static string GetFilePath<T>()
        {
            var fileName = typeof(T).Name;
            var targetFile = Path.Combine("/data", $"{fileName}.dat");
            return targetFile;
        }

        public ActionResult Save<T, V>(ConcurrentDictionary<T, V> cache)
        {
            var targetFile = GetFilePath<V>();
            try
            {
                var content = JsonConvert.SerializeObject(cache.Values.ToList());
                File.WriteAllText(targetFile, content);
                return new ActionResult(ActionStatus.Success);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return new ActionResult(ActionStatus.Error, $"Exception: {exception.Message}");
            }
        }
    }
}
