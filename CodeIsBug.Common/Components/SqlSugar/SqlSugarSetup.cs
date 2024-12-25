using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using CodeIsBug.Common.Components.Configuration;
using CodeIsBug.Common.Helper;
using CodeIsBug.Common.Token;
using CodeIsBug.Models.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace CodeIsBug.Common.Components.SqlSugar;

/// <summary>
/// 注册sqlsuagr
/// </summary>
public static class SqlSugarSetup
{
    /// <summary>
    /// 注册SqlSugar
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSqlSugar(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        var connectionString = AppSettings.PGSqlServerConnection;
        var configConnection = new ConnectionConfig
        {
            DbType = DbType.PostgreSQL,
            ConnectionString = connectionString,
            IsAutoCloseConnection = true,
            ConfigId = 1
        };
        // 文档地址：https://www.donet5.com/Home/Doc?typeId=1204
        Action<SqlSugarClient> sqlclient = db =>
        {
            // 打印SQL语句
            db.Aop.OnLogExecuting = (sql, parameters) =>
            {
                var originColor = Console.ForegroundColor;
                if (sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                    Console.ForegroundColor = ConsoleColor.Green;
                if (sql.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) ||
                    sql.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                    Console.ForegroundColor = ConsoleColor.Yellow;
                if (sql.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase))
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("【" + DateTime.Now + "——执行SQL】\r\n" +
                                  UtilMethods.GetSqlString(db.CurrentConnectionConfig.DbType, sql, parameters) +
                                  "\r\n");
                Console.ForegroundColor = originColor;
            };
            //数据处理：增、删 和 改
            db.Aop.DataExecuting = (oldValue, entityInfo) =>
            {
                try
                {
                    var tokenServer = App.GetService<ITokenService>();
                    var user = tokenServer?.GetCurrentUserInfo().Result;
                    if (entityInfo.OperationType == DataFilterType.InsertByObject)
                    {
                        if (entityInfo.PropertyName == "CreateUserId")
                        {
                            if (user != null) entityInfo.SetValue(user.UserId);
                        }
                        
                    }
                    else if (entityInfo.OperationType == DataFilterType.UpdateByObject)
                    {
                        if (entityInfo.PropertyName == "LastModifyUserId")
                            if (user != null)
                                entityInfo.SetValue(user.UserId);
                    }
                }
                catch
                {
                    // ignored
                }
            };
            //查询事件 
            db.Aop.DataExecuted = (value, entity) => { };

            db.Aop.OnError = async ex =>
            {
                if (ex.Parametres == null) return;
                var originColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                var pars = db.Utilities.SerializeObject(
                    ((SugarParameter[])ex.Parametres).ToDictionary(it => it.ParameterName, it => it.Value));
                Console.ForegroundColor = originColor;
                Console.WriteLine("【" + DateTime.Now + "——执行SQL异常】\r\n" + pars + " \r\n");
                await IoFileHelper.WriteAsync("sqlerror/", ex.ToJson());
            };

            //监控所有超过1秒的Sql 
            db.Aop.OnLogExecuted =  async (sql, p) =>
            {
                if (db.Ado.SqlExecutionTime.TotalSeconds > 1)
                {
                    //代码CS文件名
                    var fileName = db.Ado.SqlStackTrace.FirstFileName;
                    //代码行数
                    var fileLine = db.Ado.SqlStackTrace.FirstLine;
                    //方法名
                    var firstMethodName = db.Ado.SqlStackTrace.FirstMethodName;
                    //db.Ado.SqlStackTrace.MyStackTraceList[1].xxx 获取上层方法的信息

                    Console.WriteLine("【" + DateTime.Now + "——执行SQL超时】\r\n" + fileName + " \r\n");
                    await IoFileHelper.WriteAsync("sqlexcution/", fileName + sql + fileLine + firstMethodName);
                }

                ;
            };


            //全局过滤器
            var types = GetSugarTableTypes();
            // 配置加删除全局过滤器
            foreach (var entityType in types)
                if (!entityType.GetProperty("IsDelete").IsEmpty())
                {
                    //判断实体类中包含IsDeleted属性
                    //构建动态Lambda
                    var lambda = DynamicExpressionParser.ParseLambda([Expression.Parameter(entityType, "it")],
                        typeof(bool), $"{nameof(BaseModel.IsDelete)} ==  @0", false);
                    db.QueryFilter.Add(new TableFilterItem<object>(entityType, lambda)
                    {
                        IsJoinQuery = true
                    }); //将Lambda传入过滤器
                }
        };

        //SqlSugarScope线程是安全的
        var sqlSugar = new SqlSugarScope(configConnection, sqlclient);

        //这边是SqlSugarScope用AddSingleton
        services.AddSingleton<ISqlSugarClient>(sqlSugar);

        // 注册 SqlSugar 仓储
       
        return services;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static List<Type> GetSugarTableTypes()
    {
        string[] arr = ["CodeIsBug.Repository"];
        var assemblies = AssemblyHelper.GetAssemblies(arr);
        var types = new List<Type>();

        foreach (var assembly in assemblies)
        {
            // 获取所有类型
            var assemblyTypes = assembly.GetTypes();

            // 筛选出带有SugarTable特性的非抽象类
            types.AddRange(assemblyTypes.Where(type =>
                    !type.IsAbstract &&
                    type.IsDefined(typeof(SugarTable), true) // true 表示从继承层次结构中搜索特性
            ));
        }

        return types;
    }
}