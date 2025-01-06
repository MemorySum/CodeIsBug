namespace CodeIsBug.Repository.IRepository;

/// <summary>
/// 基础仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseFreeSqlRepository<T> where T : class, new()
{
   
    #region 属性

    /// <summary>
    /// 初始化 FreeSql 客户端
    /// </summary>
    private readonly IFreeSql _freeSql;
    
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="freeSql"></param>
    protected BaseFreeSqlRepository(IFreeSql freeSql)
    {
        _freeSql = freeSql;
    }

    #endregion

    
}