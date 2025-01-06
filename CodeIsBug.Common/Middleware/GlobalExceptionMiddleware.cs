using CodeIsBug.Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace CodeIsBug.Common.Middleware;

/// <summary>
/// 全局异常中间件
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
/// <param name="freeSql"></param>
public class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger,
    IFreeSql freeSql)
{
    private readonly IFreeSql _freeSql = freeSql ?? throw new ArgumentNullException(nameof(freeSql));

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context); // 调用下一个中间件
        }
        catch (Exception ex)
        {
            logger.LogError($"Exception: {ex.Message}, Path: {context.Request.Path}, Trace: {ex.StackTrace}");

            await HandleExceptionAsync(context, ex); // 处理异常
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is UnauthorizedAccessException)
        {
            var APIResults = new ApiResult(StatusCodes.Status401Unauthorized, "Unauthorized access",
                exception.Message);
            // 设置响应的Content-Type为application/json
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(APIResults.ToJson(true));
        }
        else if (exception is ArgumentException)
        {
            var APIResults = new ApiResult(StatusCodes.Status400BadRequest, "Bad request", exception.Message);
            // 设置响应的Content-Type为application/json
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(APIResults.ToJson(true));
        }
        else
        {
            var APIResults = new ApiResult(StatusCodes.Status500InternalServerError, "Internal Server Error",
                exception.Message);
            // 设置响应的Content-Type为application/json
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(APIResults.ToJson(true));
        }
    }
}