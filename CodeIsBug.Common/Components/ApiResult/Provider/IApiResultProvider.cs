
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Common;

public interface IAPIResultsProvider
{
    IActionResult ProcessActionResult(IActionResult actionResult);
    IActionResult ProcessAPIResultsException(ApiResultException resultException);
}