using Microsoft.AspNetCore.Http;

namespace CodeIsBug.Service.Services;

/// <summary>
/// 用户service
/// </summary>
/// <param name="tokenService"></param>
/// <param name="userRepository"></param>
/// <param name="userRoleMapRepository"></param>
/// <param name="rolePermissionMapRepository"></param>
public class UserService(
    ITokenService tokenService,
    UserRepository userRepository,
    UserRoleMapRepository userRoleMapRepository,
    RolePermissionMapRepository rolePermissionMapRepository)
{
    /// <summary>
    /// 登录功能
    /// </summary>
    /// <param name="loginInputDto"></param>
    /// <returns></returns>
    public async Task<ApiResult> Login(LoginInputDto loginInputDto)
    {
        TokenData userLoginOut = new TokenData();
        var user = await userRepository.LoginAsync(loginInputDto);
        if (ReferenceEquals(user, null))
        {
            return new ApiResult(StatusCodes.Status200OK, "用户名或密码错误");
        }

        var d = await rolePermissionMapRepository.GetUserRolePermissionMap(user.Id);
        var roleList = await userRoleMapRepository.GetUserRoleIdsAsync(user.Id);
        userLoginOut.UserId = user.Id;
        userLoginOut.UserName = user.UserName;
        userLoginOut.UserAccount = user.NickName;
        userLoginOut.UserRolesId = roleList.Select(a => a.RoleId).ToList();
        userLoginOut.IsSuperAdmin = roleList.Any(a => a.RoleCode?.ToLower() == "superadmin");
        userLoginOut.UserPermiss = await rolePermissionMapRepository.GetUserRolePermissionMap(user.Id);
        var token = await tokenService.GenerateTokenAsync(userLoginOut);
        return new ApiResult(StatusCodes.Status200OK, "登录成功", token);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ApiResult> GetUserInfo(string token)
    {
        var tokenData = await tokenService.GetCurrentUserInfo();
        return new ApiResult(StatusCodes.Status200OK, "用户信息获取成功", tokenData);
    }
}