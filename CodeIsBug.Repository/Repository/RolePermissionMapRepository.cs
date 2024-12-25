using CodeIsBug.Models.Admin;
using CodeIsBug.Repository.IRepository;
using SqlSugar;

namespace CodeIsBug.Repository.Repository;

/// <summary>
/// 角色权限对照表仓储
/// </summary>
/// <param name="db"></param>
public class RolePermissionMapRepository(ISqlSugarClient db) : BaseSqlsugarRepository<RolePermissionMap>(db)
{
    /// <summary>
    /// 获取用户对应角色的权限清单
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<string>> GetUserRolePermissionMap(Guid userId)
    {
        return await db.Queryable<User, UserRoleMap, RolePermissionMap, Permission>(
                (u, ur, rpm, p) => new JoinQueryInfos(
                    JoinType.Left, u.Id == ur.UserId,
                    JoinType.Left, ur.RoleId == rpm.RoleId,
                    JoinType.Left, rpm.PermissionId == p.Id
                ))
            .Select((u, ur, rpm, p) => new
            {
                p.Code
            }).ToListAsync(a => a.Code);
    }
}