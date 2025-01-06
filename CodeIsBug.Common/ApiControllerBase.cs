﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Common;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")] //返回数据的格式 直接约定为Json
[Authorize] //接口全部需要登录,特殊的除外
public class ApiControllerBase : ControllerBase
{

}