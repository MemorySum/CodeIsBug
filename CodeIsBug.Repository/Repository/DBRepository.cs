using System.Reflection;

namespace CodeIsBug.Repository.Repository
{
    public class DBRepository(IFreeSql freeSql)
    {
        /// <summary>
        /// 
        /// </summary>
        public async void SyncDb()
        {
            
            Type[] types = Assembly.Load("CodeIsBug.Models") //如果 .dll报错，可以换成 xxx.exe 有些生成的是exe 
                .GetTypes()
                .Where(it => it.FullName != null &&
                             it.FullName.Contains("CodeIsBug.Models.Admin.")) //命名空间过滤，可以写其他条件
                .ToArray(); //断点调试一下是不是需要的Type，不是需要的在进行过滤

            freeSql.CodeFirst.SyncStructure(types);
        }

        /// <summary>
        /// 初始化种子数据
        /// </summary>
        public async Task InitDataAsync()
        {
            Guid userId = Guid.NewGuid();
            Guid roleId = Guid.NewGuid();
            if (!await freeSql.Queryable<User>().AnyAsync())
            {
                User user = new User
                {
                    Id = userId,
                    UserName = "admin",
                    Password = "admin".Md5Hash(),
                    Avatar = String.Empty,
                    Remark = String.Empty,
                    Status = 1,
                    NickName = "超级管理员"
                };
                await freeSql.Insert<User>(user).ExecuteAffrowsAsync();
            }

            if (!await freeSql.Queryable<Role>().AnyAsync())
            {
                Role role = new Role()
                {
                    Id = roleId,
                    Sort = 1,
                    Description = "超级管理员",
                    Code = "superAdmin",
                    Name = "管理员",
                    Enabled = 1
                };
                await freeSql.Insert<Role>(role).ExecuteAffrowsAsync();
            }

            if (!await freeSql.Queryable<UserRoleMap>().AnyAsync())
            {
                UserRoleMap role = new UserRoleMap()
                {
                    Id = new Guid(),
                    UserId = userId,
                    RoleId = roleId
                };
                await freeSql.Insert<UserRoleMap>(role).ExecuteAffrowsAsync();
            }
        }
    }
}