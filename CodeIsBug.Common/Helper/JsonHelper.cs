using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CodeIsBug.Common.Helper;

public static class JsonHelper
{
    /// <summary>
    /// 字符串序列化对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T? ToDeserializeObject<T>(this string obj)
    {
        return JsonConvert.DeserializeObject<T>(obj);
    }

    /// <summary>
    /// 对象转Json
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    /// <summary>
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="contractResolver"></param>
    /// <returns></returns>
    public static string ToJson(this object obj, bool contractResolver)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        return JsonConvert.SerializeObject(obj, settings);
    }
}