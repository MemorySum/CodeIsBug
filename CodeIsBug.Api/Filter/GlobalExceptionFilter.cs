using CodeIsBug.Common;
using CodeIsBug.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeIsBug.Api.Filter;

/// <summary>
/// 全局异常过滤器
/// </summary>
public abstract class GlobalExceptionFilter : IAsyncExceptionFilter, IOrderedFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    protected GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        try
        {
            var actionMethod = (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo;

            // var sysLogEx = new TSysLogErr
            // {
            //     ExceptionType = "后台异常",
            //     ActionName = actionMethod.Name,
            //     Message = context.Exception.ToJson(),
            //     LogDateTime = DateTime.Now
            // };
            //
            // Console.WriteLine($"处理 {DateTime.Now} : {sysLogEx.ToJson()}");

            // _db.Insertable(sysLogEx).SplitTable().ExecuteReturnSnowflakeId();
        }
        catch (Exception e)
        {
            //异常进行记录 
            await IoFileHelper.WriteAsync("error/", e.ToJson());
        }
        // 设置为true，表示异常已经被处理了
        context.ExceptionHandled = true;

        //弹出500异常
        var apiResult = new ApiResult(StatusCodes.Status500InternalServerError, context.Exception.Message, "");
        // 如果是结果异常
        //IActionResult result = new ObjectResult(ApiResult) { StatusCode = StatusCodes.Status200OK };
        context.Result = new JsonResult(apiResult);
    }

    /// <summary>
    /// 最先执行
    /// </summary>
    public int Order => int.MinValue;
}