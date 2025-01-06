using CodeIsBug.Common;
using CodeIsBug.Common.Model;
using CodeIsBug.Common.Token;
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
    private readonly ITokenService _tokenService;

    /// <summary>
    /// /用户服务
    /// </summary>
    /// <param name="userService">用户服务</param>
    /// <param name="tokenService">token服务</param>
    public AccountController(UserService userService,ITokenService tokenService)
    {
        this._userService = userService;
        this._tokenService = tokenService;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="loginInputDto"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<object> Login([FromForm] LoginInputDto loginInputDto)
    {
        return await _userService.Login(loginInputDto);
    }

    /// <summary>
    /// 获取登录用户信息
    /// </summary>
    /// <param name="token">token</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<TokenData> GetUserInfo(string token)
    {
       return await _tokenService.GetCurrentUserInfo();
    }
}