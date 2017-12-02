using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCommon.Extensions
{
    /// <summary>
    /// Json操作
    /// </summary>
    public static class ExJson
    {
        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        public static T ToObject<T>(this string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default(T);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T ToModel<T>(this string json)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(json))
                    return default(T);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static public T ToModel<T>(this string s, T model)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static public object ToModel(this string json, Type type)
        {
            try
            {
                return JsonConvert.DeserializeObject(json, type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="isConvertSingleQuotes">是否将双引号转成单引号</param>
        public static string ToJson(this object target, bool isConvertSingleQuotes = false)
        {
            if (target == null) return "";
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsSettings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            });
            //忽略空值
            jsSettings.NullValueHandling = NullValueHandling.Ignore;
            var result = JsonConvert.SerializeObject(target, Formatting.None, jsSettings);
            if (isConvertSingleQuotes)
                result = result.Replace("\"", "'");
            return result;
        }

        /// <summary>
        /// 将对象转换为Json字符串，并且去除两侧括号
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="isConvertSingleQuotes">是否将双引号转成单引号</param>
        public static string ToJsonWithoutBrackets(this object target, bool isConvertSingleQuotes = false)
        {
            var result = ToJson(target, isConvertSingleQuotes);
            if (result == "{}")
                return result;
            return result.TrimStart('{').TrimEnd('}');
        }

        /// <summary>
        ///   n1=value1&n2=value2  转Json 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static public string ToJsonByForm(this string s)
        {
            Dictionary<string, string> dicdata = new Dictionary<string, string>();
            try
            {
                var data = s.Split('&');
                for (int i = 0; i < data.Length; i++)
                {
                    var dk = data[i].Split('=');
                    StringBuilder sb = new StringBuilder(dk[1]);
                    for (int j = 2; j <= dk.Length - 1; j++)
                        sb.Append(dk[j]);
                    dicdata.Add(dk[0], sb.ToString());
                }
            }
            catch
            {
            }
            return dicdata.ToJson();
        }


        /// <summary>
        /// 将字典类型序列化为json字符串
        /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="dict">要序列化的字典数据</param>
        /// <returns>json字符串</returns>
        public static string SerializeDictionaryToJsonString<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            if (dict.Count == 0)
                return "";

            string jsonStr = JsonConvert.SerializeObject(dict);
            return jsonStr;
        }

        /// <summary>
        /// 将json字符串反序列化为字典类型
        /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>字典数据</returns>
        public static Dictionary<TKey, TValue> DeserializeStringToDictionary<TKey, TValue>(this string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();

            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);

            return jsonDict;

        }

    }

}
