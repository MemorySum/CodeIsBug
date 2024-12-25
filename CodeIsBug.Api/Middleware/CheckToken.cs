using System.Text.RegularExpressions;
using CodeIsBug.Common;
using CodeIsBug.Common.Attributes;
using CodeIsBug.Common.Helper;
using CodeIsBug.Common.Token;
using Microsoft.AspNetCore.Authorization;

namespace CodeIsBug.Api.Middleware;

public class CheckToken
{
    private readonly RequestDelegate _next;

    public CheckToken(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 获取当前请求的Endpoint
        var endpoint = context.GetEndpoint();

        //一些禁用的资源放行检查
        var displayNameStr = new[]
        {
            "/hub" //
        };

        // 检查Endpoint的元数据中是否包含AllowAnonymous特性 
        var hasAllowAnonymous = endpoint?.Metadata
            .OfType<IAllowAnonymous>()
            .Any() ?? true;

        //静态资源直接放行 
        //禁用检查的资源放行
        //匿名访问的资源直接放行
        if (endpoint is null || displayNameStr.Contains(endpoint.DisplayName) || hasAllowAnonymous)
        {
            await _next(context);
            return;
        }
        //当前token是否在缓存中
        var tokenService = App.GetService<ITokenService>();
        //获取token
        var token = await tokenService.GetHeadersToken();
        //是否有效
        var validataToken = await tokenService.ValidateToken(token);
        if (!validataToken)
        {
            throw new UnauthorizedAccessException("提供的令牌无效或已过期，请重新登录");
        }
        //刷新用户的token过期时间
        await tokenService.RefreshTokenAsync(token);

        var sysuser = await tokenService.ParseTokenAsync(token);
        if (sysuser is null)
        {
            throw new UnauthorizedAccessException("提供的令牌无效或已过期，请重新登录");
        }
        await _next(context);
    }

    

    /// <summary>
    /// 登录后返回暂无权限
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Res207Async(HttpContext context)
    {
        var apiResult = new ApiResult(StatusCodes.Status207MultiStatus, "暂无权限", "");

        // 设置响应的Content-Type为application/json
        context.Response.ContentType = "application/json";
        // 写入JSON字符串到响应体
         await context.Response.WriteAsync(apiResult.ToJson(true));
         return;
    }
}