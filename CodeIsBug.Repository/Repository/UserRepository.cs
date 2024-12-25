using CodeIsBug.Common.Helper;
using CodeIsBug.Models.Admin;
using CodeIsBug.Models.Dto;
using CodeIsBug.Repository.IRepository;
using SqlSugar;

namespace CodeIsBug.Repository.Repository;

public class UserRepository(ISqlSugarClient db) : BaseSqlsugarRepository<User>(db)
{
    public async Task<User> Login(LoginInputDto loginInputDto)
    {
        return await db.Queryable<User>()
            .Where(a => a.UserName == loginInputDto.UserName && a.Password == loginInputDto.Password.Md5Hash())
            .FirstAsync();
    }

    public async Task InitData()
    {
        Guid id = Guid.NewGuid();
        Guid rid = Guid.NewGuid();
        var d = await db.Queryable<User>().AnyAsync();
        var rd = await db.Queryable<Role>().AnyAsync();
        var urd = await db.Queryable<UserRoleMap>().AnyAsync();
        if (!d)
        {
            var u = await db.Insertable<User>(new User
            {
                Avatar = string.Empty,
                IsDelete = false,
                LastModifyTime = null,
                LastModifyUserId = null,
                Enabled = true,
                CreateTime = DateTime.Now,
                CreateUserId = id,
                NickName = "superadmin",
                Password = "123456".Md5Hash(),
                Remark = string.Empty,
                Sort = 1,
                Status = 1,
                UserName = "系统超级管理员"
            }).ExecuteReturnEntityAsync();
        }
        if (!rd)
        {
            var nd = await db.Insertable<Role>(new Role
            {
                Code = "admin",
                CreateTime = DateTime.Now,
                CreateUserId = id,
                Description = "管理员",
                Enabled = 1,
                Id = rid,
                IsDelete = false,
                LastModifyTime = null,
                LastModifyUserId = null,
                Name = "管理员",
                Sort = 1,
            }).ExecuteReturnEntityAsync();
        }

        if (!urd)
        {
            await db.Insertable<UserRoleMap>(new UserRoleMap
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,

                CreateUserId = id,
                RoleId = rid,
                UserId = id
            }).ExecuteReturnEntityAsync();
        }

    }
}