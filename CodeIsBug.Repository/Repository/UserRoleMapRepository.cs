using CodeIsBug.Models.Admin;
using CodeIsBug.Models.Dto;
using CodeIsBug.Repository.IRepository;
using SqlSugar;

namespace CodeIsBug.Repository.Repository;

/// <summary>
/// 用户角色对照表仓储
/// </summary>
/// <param name="db"></param>
public  class UserRoleMapRepository(ISqlSugarClient db) : BaseSqlsugarRepository<UserRoleMap>(db)
{
    public async Task<List<UserRoleDto>> GetUserRoleIdsAsync(Guid userId)
    {
        var list = await db.Queryable<UserRoleMap>()
            .LeftJoin<Role>((urm, role) => urm
                .RoleId == role.Id)
            .Where((urm, r) => urm.UserId == userId)
            .Select((urm, r) => new UserRoleDto
            {
                UserId = urm.UserId,
                RoleId = urm.RoleId,
                RoleCode = r.Code,
                RoleName = r.Name
            }).ToListAsync();
        return list;
    }
}