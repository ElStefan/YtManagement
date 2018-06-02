﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YtManagement.Model;

namespace YtManagement.Monitor
{
    public class YtManagementClient
    {
        private static Uri ApiUri;

        public static ActionResult<int> AddRule(ManagementRule rule)
        {
            return Put<ActionResult<int>>("rules", rule);
        }
        public static ActionResult UpdateRule(ManagementRule rule)
        {
            return Post<ActionResult>("rules", rule);
        }
        public static ActionResult DeleteRule(int id)
        {
            return Delete<ActionResult>("rules", id);
        }
        public static ActionResult<ManagementRule> GetRule(int id)
        {
            return Get<ActionResult<ManagementRule>>("rules", id);
        }
        public static ActionResult<List<ManagementRule>> GetRules()
        {
            return Get<ActionResult<List<ManagementRule>>>("rules");
        }

        private static T Get<T>(string path)
        {
            return Get<T>(path, null);
        }

        private static T Get<T>(string path, object id)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    if (id != null)
                    {
                        path = path + "/" + id;
                    }
                    var response = httpClient.GetAsync(ApiUri + "/" + path).Result;

                    var content = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch (Exception exception)
                {
                    return (T)Activator.CreateInstance(typeof(T), ActionStatus.Error, "Exception on request: " + exception.Message);
                }

            }
        }
        private static T Post<T>(string path, object item)
        {
            using (var httpClient = new HttpClient())
            {
                using (var sendContent = CreateObject(item))
                {
                    try
                    {
                        var response = httpClient.PostAsync(ApiUri + "/" + path, sendContent).Result;

                        var content = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                    catch (Exception exception)
                    {
                        return (T)Activator.CreateInstance(typeof(T), ActionStatus.Error, "Exception on request: " + exception.Message);
                    }
                }
            }
        }
        private static T Put<T>(string path, object item)
        {
            using (var httpClient = new HttpClient())
            {
                using (var sendContent = CreateObject(item))
                {
                    try
                    {
                        var response = httpClient.PutAsync(ApiUri + "/" + path, sendContent).Result;

                        var content = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                    catch (Exception exception)
                    {
                        return (T)Activator.CreateInstance(typeof(T), ActionStatus.Error, "Exception on request: " + exception.Message);
                    }
                }
            }
        }
        private static T Delete<T>(string path, object id)
        {
            using (var httpClient = new HttpClient())
            {

                try
                {
                    if (id != null)
                    {
                        path = path + "/" + id;
                    }
                    var response = httpClient.DeleteAsync(ApiUri + "/" + path).Result;

                    var content = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch (Exception exception)
                {
                    return (T)Activator.CreateInstance(typeof(T), ActionStatus.Error, "Exception on request: " + exception.Message);
                }

            }
        }

        private static StringContent CreateObject(object rule)
        {
            return new StringContent(JsonConvert.SerializeObject(rule), Encoding.UTF8, "application/json");
        }

        public static void SetApiUri(string url)
        {
            ApiUri = new Uri(url);
        }
    }
}