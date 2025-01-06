using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Microsoft.Extensions.DependencyInjection;

namespace CodeIsBug.Common.Components.JsonOptions.SystemTextJson;

/// <summary>
/// 注入json序列化
/// </summary>
public static class JsonOptionsExtensions
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddTextJsonOptions(this IServiceCollection services,
        Action<JsonSerializerOptions>? setupAction = null)
    {

         
        // 配置 JsonOptions
        services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
        {
            // 内部函数 配置 JsonSerializerOptions
            JsonSerializerOptions ConfigureJsonOptions(JsonSerializerOptions options)
            {
                // 驼峰命名
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                // Unicode 编码
                options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

                // 忽略循环引用
                // https://docs.microsoft.com/zh-cn/dotnet/standard/serialization/system-text-json-preserve-references
                options.ReferenceHandler = ReferenceHandler.IgnoreCycles;

                // 自定义 Converter
                options.Converters.Add(new DateTimeJsonConverter());
                options.Converters.Add(new EnumJsonConverter());

                // 如果传入自定义配置
                if (setupAction != null) setupAction(options);

                return options;
            }

        });
             

        return services;
    }
}