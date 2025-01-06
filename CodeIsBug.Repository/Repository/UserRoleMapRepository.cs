namespace CodeIsBug.Repository.Repository;

/// <summary>
/// 角色对照仓储
/// </summary>
/// <param name="freeSql"></param>
public  class UserRoleMapRepository(IFreeSql freeSql) : BaseFreeSqlRepository<UserRoleMap>(freeSql)
{
    private readonly IFreeSql _freeSql = freeSql;

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<UserRoleDto>> GetUserRoleIdsAsync(Guid userId)
    {
        var list = await _freeSql.Select<UserRoleMap, Role>()
            .LeftJoin<Role>((urm, role) => urm.RoleId == role.Id)
            .Where(u => u.t1.UserId == userId)
            .ToListAsync<UserRoleDto>((urm, role) => new UserRoleDto
            {
                UserId = urm.UserId,
                RoleId = urm.RoleId,
                RoleCode = role.Code,
                RoleName = role.Name
            });
        return list;
    }
}