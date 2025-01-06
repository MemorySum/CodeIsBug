using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Common;

public class APIResultsProvider : IAPIResultsProvider
{
    public APIResultsProvider()
    {
    }

    public virtual IActionResult ProcessActionResult(IActionResult actionResult)
    {
        // 只处理 ObjectResult
        if (actionResult is ObjectResult objectResult)
        {
            // 已经是统一返回结果，直接返回
            if (objectResult.Value is ApiResult) return actionResult;

            // 其他情况，包装成 API 统一返回结果
            var statusCode = objectResult.StatusCode ?? StatusCodes.Status200OK;
            var message = ApiResultsHelper.GetMessage(statusCode);
            var APIResults = ApiResultsHelper.Result(statusCode, message, objectResult.Value);
            var newResult = new ObjectResult(APIResults);
            return newResult;
        }

        return actionResult;
    }

    public virtual IActionResult ProcessAPIResultsException(ApiResultException resultException)
    {
        // API 结果 统一处理为 ObjectResult 与 API 控制器保持一致
        var APIResults = resultException.ApiResult;
        IActionResult result = new ObjectResult(APIResults) { StatusCode = APIResults.Code };

        return result;
    }
}