using CodeIsBug.Common.Components.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeIsBug.Common.Components.FreeSqlSetup;

/// <summary>
/// 注入FreeSql
/// </summary>
public static class FreeSqlSetup
{
    /// <summary>
    /// 注入FreeSql
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddFreeSql(this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

        IFreeSql FsqlFactory(IServiceProvider r)
        {
            IFreeSql fsql = new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.MySql, AppSettings.ConnectionString)
                .UseAdoConnectionPool(true)
                .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))
                .UseAutoSyncStructure(true) //自动同步实体结构到数据库，只有CRUD时才会生成表
                .Build();
            return fsql;
        }

        serviceCollection.AddSingleton<IFreeSql>((Func<IServiceProvider, IFreeSql>)FsqlFactory);
        return serviceCollection;
    }
}