using CodeIsBug.Common.Model;
using CodeIsBug.Common.Token;
using CodeIsBug.Models.Admin;
using CodeIsBug.Models.Dto;
using CodeIsBug.Repository.Repository;

namespace CodeIsBug.Service.Services;

public class UserService(
    ITokenService tokenService,
    UserRepository userRepository,
    UserRoleMapRepository userRoleMapRepository,
    RolePermissionMapRepository rolePermissionMapRepository)
{
    public async Task<TokenData> Login(LoginInputDto loginInputDto)
    {
        TokenData userLoginOut = new TokenData();
        var user = await userRepository.Login(loginInputDto);
        var roleList = await userRoleMapRepository.GetUserRoleIdsAsync(user.Id);
        userLoginOut.UserId = user.Id;
        userLoginOut.UserName = user.UserName;
        userLoginOut.UserRolesId = roleList.Select(a => a.RoleId).ToList();
        userLoginOut.IsSuperAdmin = roleList.Any(a => a.RoleName?.ToLower() == "superadmin");
        userLoginOut.UserPermiss = await rolePermissionMapRepository.GetUserRolePermissionMap(user.Id);
        userLoginOut.Token = await tokenService.GenerateTokenAsync(userLoginOut);
        return userLoginOut;
    }

    public async Task InitData()
    {
       
    }
}