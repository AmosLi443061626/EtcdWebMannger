using System;
using Microsoft.AspNetCore.Mvc;
using EtcdGrcpClient;
using CoreCommon.Extensions;
using System.Collections.Generic;

namespace EtcdManngerWeb.Controllers
{
    /// <summary>
    /// Etcd操作
    /// </summary>
    [Produces("application/json")]
    [Route("etcd/[action]")]
    public class EtcdController : Controller
    {
        static string etcdUri = "";
        static EtcdClient etcdClient;

        /// <summary>
        /// Etcd 设置连接地址
        /// </summary>
        /// <returns>true/False</returns>
        [HttpPost]
        public string EtcdSet(string url)
        {
            try
            {
                etcdUri = url;
                if (etcdClient != null)
                {
                    etcdClient.Dispose();
                }
                etcdClient = new EtcdClient(new Uri(etcdUri));

                return "ok";
            }
            catch (Exception ex)
            {
                return "error:" + ex.Message;
            }
        }

        /// <summary>
        /// 设置etcd值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool SetNodes(string key, string value)
        {
            if (key.IndexOf('/') != 0)
            {
                key = "/" + key;
            }
            var rep = etcdClient.Put(key, value).Result;
            return true;
        }

        /// <summary>
        /// 获取Etcd值下级
        /// </summary>
        /// <param name="key">要删除的Key</param>
        /// <returns>json</returns>
        [HttpPost]
        public string Get(string key)
        {
            if (key.IsNullOrEmpty())
            {
                return etcdClient.GetRange("/").Result.ToJson();
            }
            if (key.IndexOf('/') != 0)
            {
                key = "/" + key;
            }
            var dic = etcdClient.GetRange(key).Result;
            return dic.ToJson();
        }

        /// <summary>
        /// 删除Etcd值包括下级
        /// </summary>
        /// <param name="key">需要删除的Key(/ 斜杠开头与结尾)</param>
        /// <returns></returns>
        [HttpPost]
        public bool Delete(string key)
        {
            if (key.IndexOf('/') != 0)
            {
                key = "/" + key;
            }
            var rep = etcdClient.DeleteRange(key).Result;
            return true;
        }

        /// <summary>
        /// 加载一个json文件写入配置中心
        /// </summary>
        /// <param name="key">更新地址</param>
        /// <param name="settingJson">Json</param>
        /// <returns></returns>
        [HttpPost]
        public bool Load(string key, string settingJson)
        {
            if (key.IndexOf('/') != 0)
            {
                key = "/" + key;
            }
            if (key.LastIndexOf("/") != key.Length - 1)
            {
                key = key + "/";
            }
            Dictionary<string, string> dictionary = settingJson.XElmentToConfJson();
            foreach (var item in dictionary)
                etcdClient.Put(key + item.Key, item.Value);
            return true;
        }
    }
}