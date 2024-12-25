using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodeIsBug.Models.Admin;
using CodeIsBug.Models.Models;
using CodeIsBug.Repository.IRepository;
using SqlSugar;

namespace CodeIsBug.Repository.Repository
{
    public class DBRepository(ISqlSugarClient db) : BaseSqlsugarRepository<User>(db)
    {
        public async void SyncDB()
        {
            Type[] types = Assembly.Load("CodeIsBug.Models") //如果 .dll报错，可以换成 xxx.exe 有些生成的是exe 
                .GetTypes()
                .Where(it => it.FullName != null &&
                             it.FullName.Contains("CodeIsBug.Models.Admin.")) //命名空间过滤，可以写其他条件
                .ToArray(); //断点调试一下是不是需要的Type，不是需要的在进行过滤

            db.CodeFirst.SetStringDefaultLength(200).InitTables(types); //根据types创建表
        }
    }
}