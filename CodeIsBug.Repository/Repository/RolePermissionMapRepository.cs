namespace CodeIsBug.Repository.Repository;

/// <summary>
/// 角色权限对照表仓储
/// </summary>
public  class RolePermissionMapRepository(IFreeSql freeSql) : BaseFreeSqlRepository<RolePermissionMap>(freeSql)
{
    private readonly IFreeSql _freeSql = freeSql;

    /// <summary>
    /// 获取用户对应角色的权限清单
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<string>> GetUserRolePermissionMap(Guid userId)
    {
        return await _freeSql.Select<User, UserRoleMap, RolePermissionMap, Permission>()
            .LeftJoin((u, ur, rp, p) => u.Id == ur.UserId)
            .LeftJoin((u, ur, rp, p) => ur.RoleId == rp.RoleId)
            .LeftJoin((u, ur, rp, p) => rp.RoleId == p.Id)
            .Where((u, ur, rp, p) => u.Id == userId)
            .Where((u, ur, rp, p) => p.Enabled == true)
            .ToListAsync((u, ur, rp, p) => p.Code);
    }
}