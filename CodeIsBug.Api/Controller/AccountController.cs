using CodeIsBug.Common;
using CodeIsBug.Common.Model;
using CodeIsBug.Models.Dto;
using CodeIsBug.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Api.Controller;

/// <summary>
/// 用户账户控制器
/// </summary>
[ApiExplorerSettings(GroupName = "后台管理")]
public class AccountController : ApiControllerBase
{
    private readonly UserService _userService;

    /// <summary>
    /// /用户服务
    /// </summary>
    /// <param name="userService"></param>
    public AccountController(UserService userService)
    {
        this._userService = userService;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="loginInputDto"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<TokenData> Login([FromBody] LoginInputDto loginInputDto)
    {
        return await _userService.Login(loginInputDto);
    }
    
}