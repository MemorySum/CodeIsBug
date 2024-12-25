
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeIsBug.Common;

public static class APIResultsMvcBuilderExtensions
{
    /// <summary>
    /// 配置 API 统一返回结果
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="filterOrder">结果过滤器排序</param>
    /// <returns></returns>
    public static IMvcBuilder AddApiResult(this IMvcBuilder builder)
    {
        builder.AddApiResult();

        return builder;
    }

    /// <summary>
    /// 配置 API 统一返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <param name="filterOrder">结果过滤器排序</param>
    /// <returns></returns>
    public static IMvcBuilder AddApiResult<T>(this IMvcBuilder builder) where T : IAPIResultsProvider
    {
        // 添加过滤器
        builder.AddMvcOptions(options =>
        {
            // 过滤器管道（先进后出）顺序：Middleware >> ExceptionFilter >> ActionFilter
            // 异常触发顺序：ActionFilter 先于 ExceptionFilter
            // 因为要配合 RequestActionFilter 的使用，所以不能用 ExceptionFilter
            // 先配置 RequestActionFilter(Order=-8000) 再配置 AppResultActionFilter(Order=-6000)
            options.Filters.Add<ApiResultActionFilter>();
        });

        builder.Services.AddSingleton<IAPIResultsProvider, APIResultsProvider>();

        // 替换 IAPIResultsProvider 的默认实现
        builder.Services.Replace(
            new ServiceDescriptor(typeof(IAPIResultsProvider), typeof(T), ServiceLifetime.Singleton));

        return builder;
    }
}