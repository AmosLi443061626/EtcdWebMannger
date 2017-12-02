using CoreCommon.CacheOperation;
using Microsoft.Extensions.Configuration;
using CoreCommon.Extensions;
using System.Collections.Concurrent;
using CoreCommon.RedisHelper;
using System.Collections.Generic;

namespace CoreCommon.Configs
{
    public static class ConfigManagerConf
    {
        public static IConfiguration Configuration = null;

        static ConcurrentDictionary<string, List<string>> _dicCache = new ConcurrentDictionary<string, List<string>>();

        static EtcdConfiWatcher etcdConfiWatcher;


        public static void SetConfiguration(IConfiguration configuration)
        {
            if (Configuration == null)
                Configuration = configuration;
            if (etcdConfiWatcher != null)
            {
                etcdConfiWatcher.Dispose();
            }
            if (GetValue("etcd:address") != null)
            {
                etcdConfiWatcher = new EtcdConfiWatcher(_dicCache);
            }
        }


        public static string GetValue(string key)
        {
            List<string> refValue;
            _dicCache.TryGetValue(key, out refValue);
            if (refValue != null)
                return refValue?[0];

            if (Configuration == null)
                return "";
            string value = Configuration[key];
            if (!value.IsNullOrEmpty()) //本地存在则返回
                return value;
            return "";
        }

        public static List<string> GetReferenceValue(string key)
        {
            List<string> refValue;
            _dicCache.TryGetValue(key, out refValue);
            if (refValue == null)
            {
                refValue = new List<string>();
                refValue[0] = string.Empty;
                _dicCache.TryAdd(key, refValue);
            }
            return refValue;
        }

        public static IDictionary<string, string> GetWatcherAll()
        {
            return etcdConfiWatcher.GetAll();
        }
    }
}
