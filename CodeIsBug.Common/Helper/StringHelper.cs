using System.Security.Cryptography;
using System.Text;

namespace CodeIsBug.Common.Helper;

/// <summary>
/// MD5扩展类
/// </summary>
public static class StringHelper
{
    /// <summary>
    ///     生成MD5加密字符串
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string Md5Hash(this string input)
    {
        using var md5 = MD5.Create();
        var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
        var strResult = BitConverter.ToString(result);
        return strResult.Replace("-", "").ToLower();
    }
}