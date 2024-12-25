using System.Linq.Expressions;
using CodeIsBug.Common;
using SqlSugar;

namespace CodeIsBug.Repository.IRepository;

public class BaseSqlsugarRepository<T> where T : class, new()
{
    private readonly string[] _updateIgnoreColumns = { "CreateTime", "CreateUserId" };

    #region 属性

    /// <summary>
    /// 初始化 SqlSugar 客户端
    /// </summary>
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 数据库上下文
    /// </summary>
    public ISqlSugarClient Context { get; }

    /// <summary>
    /// 独立数据库上下文
    /// </summary>
    private SqlSugarScopeProvider EntityContext { get; }

    /// <summary>
    /// 实体集合
    /// </summary>
    private ISugarQueryable<T> Entities =>
        _db.AsTenant().GetConnectionScopeWithAttr<T>().Queryable<T>();

    /// <summary>
    /// 原生 Ado 对象
    /// </summary>
    private IAdo Ado { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    public BaseSqlsugarRepository(ISqlSugarClient db)
    {
        Context = _db = db;
        EntityContext = _db.AsTenant().GetConnectionScopeWithAttr<T>();
        Ado = EntityContext.Ado;
    }

    #endregion

    #region 查询

    /// <summary>
    /// 获取总数
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public int Count(Expression<Func<T, bool>> whereExpression)
    {
        return Entities.Count(whereExpression);
    }

    /// <summary>
    /// 获取总数
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public Task<int> CountAsync(Expression<Func<T, bool>> whereExpression)
    {
        return Entities.CountAsync(whereExpression);
    }

    /// <summary>
    /// 检查是否存在
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public bool Any(Expression<Func<T, bool>> whereExpression)
    {
        return Entities.Any(whereExpression);
    }

    /// <summary>
    /// 检查是否存在
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await Entities.AnyAsync(whereExpression);
    }

    /// <summary>
    /// 通过主键获取实体
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public T Single(dynamic Id)
    {
        return Entities.InSingle(Id);
    }

    /// <summary>
    /// 获取一个实体
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public T Single(Expression<Func<T, bool>> whereExpression)
    {
        return Entities.Single(whereExpression);
    }

    /// <summary>
    /// 获取一个实体
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public async Task<T> SingleAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await Entities.SingleAsync(whereExpression);
    }

    /// <summary>
    /// 获取一个实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T> SingleAsync(dynamic id)
    {
        return await Entities.InSingleAsync(id);
    }

    /// <summary>
    /// 获取一个实体
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public T FirstOrDefault(Expression<Func<T, bool>> whereExpression)
    {
        return Entities.First(whereExpression);
    }

    /// <summary>
    /// 获取一个实体
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await Entities.FirstAsync(whereExpression);
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public List<T> ToList()
    {
        return Entities.ToList();
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public List<T> ToList(Expression<Func<T, bool>> whereExpression)
    {
        return Entities.Where(whereExpression).ToList();
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <param name="orderByExpression"></param>
    /// <param name="orderByType"></param>
    /// <returns></returns>
    public List<T> ToList(Expression<Func<T, bool>> whereExpression,
        Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
    {
        return Entities.OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression)
            .ToList();
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public Task<List<T>> ToListAsync()
    {
        return Entities.ToListAsync();
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public Task<List<T>> ToListAsync(Expression<Func<T, bool>> whereExpression)
    {
        return Entities.Where(whereExpression).ToListAsync();
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <param name="orderByExpression"></param>
    /// <param name="orderByType"></param>
    /// <returns></returns>
    public Task<List<T>> ToListAsync(Expression<Func<T, bool>> whereExpression,
        Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
    {
        return Entities.OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression)
            .ToListAsync();
    }

    #endregion

    #region 新增

    public virtual IInsertable<T> AsInsertable(T entity)
    {
        return EntityContext.Insertable(entity);
    }

    public virtual IInsertable<T> AsInsertable(params T[] entities)
    {
        return EntityContext.Insertable(entities);
    }

    /// <summary>
    /// 新增一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual int Insert(T entity)
    {
        return EntityContext.Insertable(entity).ExecuteCommand();
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual int Insert(params T[] entities)
    {
        return EntityContext.Insertable(entities).ExecuteCommand();
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual int Insert(IEnumerable<T> entities)
    {
        return EntityContext.Insertable(entities.ToArray()).ExecuteCommand();
    }

    /// <summary>
    /// 新增一条记录返回自增Id
    /// </summary>
    /// <param name="insertObj"></param>
    /// <returns></returns>
    public virtual int InsertReturnIdentity(T insertObj)
    {
        return EntityContext.Insertable(insertObj).ExecuteReturnIdentity();
    }

    /// <summary>
    /// 新增一条记录返回雪花Id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual long InsertReturnSnowflakeId(T entity)
    {
        return EntityContext.Insertable(entity).ExecuteReturnSnowflakeId();
    }

    /// <summary>
    /// 新增一条记录返回实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual T InsertReturnEntity(T entity)
    {
        return EntityContext.Insertable(entity).ExecuteReturnEntity();
    }


    /// <summary>
    /// 新增一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual Task<int> InsertAsync(T entity)
    {
        return EntityContext.Insertable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual Task<int> InsertAsync(params T[] entities)
    {
        return EntityContext.Insertable(entities).ExecuteCommandAsync();
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual Task<int> InsertAsync(IEnumerable<T> entities)
    {
        if (entities != null && entities.Any())
            return EntityContext.Insertable(entities.ToArray()).ExecuteCommandAsync();
        return Task.FromResult(0);
    }

    /// <summary>
    /// 新增一条记录返回自增Id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<long> InsertReturnIdentityAsync(T entity)
    {
        return await EntityContext.Insertable(entity).ExecuteReturnBigIdentityAsync();
    }

    /// <summary>
    /// 新增一条记录返回雪花Id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<long> InsertReturnSnowflakeIdAsync(T entity)
    {
        return await EntityContext.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
    }

    /// <summary>
    /// 新增一条记录返回实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<T> InsertReturnEntityAsync(T entity)
    {
        return await EntityContext.Insertable(entity).ExecuteReturnEntityAsync();
    }

    #endregion

    #region 更新

    /// <summary>
    /// 更新一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual int Update(T entity)
    {
        return EntityContext.Updateable(entity).IgnoreColumns(_updateIgnoreColumns).ExecuteCommand();
    }

    /// <summary>
    /// 更新多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual int Update(params T[] entities)
    {
        return EntityContext.Updateable(entities).IgnoreColumns(_updateIgnoreColumns).ExecuteCommand();
    }

    /// <summary>
    /// 更新多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual int Update(IEnumerable<T> entities)
    {
        return EntityContext.Updateable(entities.ToArray()).IgnoreColumns(_updateIgnoreColumns).ExecuteCommand();
    }

    /// <summary>
    /// 更新一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<int> UpdateAsync(T entity)
    {
        return await EntityContext.Updateable(entity).IgnoreColumns(_updateIgnoreColumns).ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新记录
    /// </summary>
    /// <param name="predicate">更新的条件</param>
    /// <param name="content">更新的内容</param>
    /// <returns></returns>
    public virtual int Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> content)
    {
        return EntityContext.Updateable(content).Where(predicate).IgnoreColumns(_updateIgnoreColumns).ExecuteCommand();
    }

    /// <summary>
    /// 更新记录
    /// </summary>
    /// <param name="predicate">更新的条件</param>
    /// <param name="content">更新的内容</param>
    /// <returns></returns>
    public virtual async Task<int> UpdateAsync(Expression<Func<T, bool>> predicate,
        Expression<Func<T, T>> content)
    {
        return await EntityContext.Updateable(content).Where(predicate).IgnoreColumns(_updateIgnoreColumns)
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual Task<int> UpdateAsync(params T[] entities)
    {
        return EntityContext.Updateable(entities).IgnoreColumns(_updateIgnoreColumns).ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual Task<int> UpdateAsync(IEnumerable<T> entities)
    {
        return EntityContext.Updateable(entities.ToArray()).IgnoreColumns(_updateIgnoreColumns).ExecuteCommandAsync();
    }

    public virtual IUpdateable<T> AsUpdateable(T entity)
    {
        return EntityContext.Updateable(entity).IgnoreColumns(_updateIgnoreColumns);
    }

    public virtual IUpdateable<T> AsUpdateable(IEnumerable<T> entities)
    {
        return EntityContext.Updateable(entities.ToArray()).IgnoreColumns(_updateIgnoreColumns);
    }

    #endregion

    #region 删除

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual int Delete(T entity)
    {
        return EntityContext.Deleteable(entity).ExecuteCommand();
    }

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public virtual int Delete(object key)
    {
        return EntityContext.Deleteable<T>().In(key).ExecuteCommand();
    }

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public virtual int Delete(params object[] keys)
    {
        return EntityContext.Deleteable<T>().In(keys).ExecuteCommand();
    }

    /// <summary>
    /// 自定义条件删除记录
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public int Delete(Expression<Func<T, bool>> whereExpression)
    {
        return EntityContext.Deleteable<T>().Where(whereExpression).ExecuteCommand();
    }

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual Task<int> DeleteAsync(T entity)
    {
        return EntityContext.Deleteable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public virtual Task<int> DeleteAsync(object key)
    {
        return EntityContext.Deleteable<T>().In(key).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public virtual Task<int> DeleteAsync(params object[] keys)
    {
        return EntityContext.Deleteable<T>().In(keys).ExecuteCommandAsync();
    }

    /// <summary>
    /// 自定义条件删除记录
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    public async Task<int> DeleteAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await EntityContext.Deleteable<T>().Where(whereExpression).ExecuteCommandAsync();
    }

    #endregion

    #region 其他

    /// <summary>
    /// 根据表达式查询多条记录
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual ISugarQueryable<T> Where(Expression<Func<T, bool>> predicate)
    {
        return AsQueryable(predicate);
    }

    /// <summary>
    /// 根据表达式查询多条记录
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual ISugarQueryable<T> Where(bool condition, Expression<Func<T, bool>> predicate)
    {
        return AsQueryable().WhereIF(condition, predicate);
    }

    /// <summary>
    /// 构建查询分析器
    /// </summary>
    /// <returns></returns>
    public virtual ISugarQueryable<T> AsQueryable()
    {
        return Entities;
    }

    /// <summary>
    /// 构建查询分析器
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual ISugarQueryable<T> AsQueryable(Expression<Func<T, bool>> predicate)
    {
        return Entities.Where(predicate);
    }

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <returns></returns>
    public virtual List<T> AsEnumerable()
    {
        return AsQueryable().ToList();
    }

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual List<T> AsEnumerable(Expression<Func<T, bool>> predicate)
    {
        return AsQueryable(predicate).ToList();
    }

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <returns></returns>
    public virtual Task<List<T>> AsAsyncEnumerable()
    {
        return AsQueryable().ToListAsync();
    }

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual Task<List<T>> AsAsyncEnumerable(Expression<Func<T, bool>> predicate)
    {
        return AsQueryable(predicate).ToListAsync();
    }

    public virtual bool IsExists(Expression<Func<T, bool>> whereExpression)
    {
        return Entities.Any(whereExpression);
    }

    public virtual Task<bool> IsExistsAsync(Expression<Func<T, bool>> whereExpression)
    {
        return Entities.AnyAsync(whereExpression);
    }

    #endregion

    #region 仓储事务

    /// <summary>
    /// 切换仓储(注意使用环境)
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>仓储</returns>
    public virtual BaseSqlsugarRepository<T>? Change<T>()
        where T : class, new()
    {
        return App.GetService<BaseSqlsugarRepository<T>>();
    }

    /// <summary>
    /// 当前db
    /// </summary>
    public void CurrentBeginTran()
    {
        Ado.BeginTran();
    }

    /// <summary>
    /// 当前db
    /// </summary>
    public void CurrentCommitTran()
    {
        Ado.CommitTran();
    }

    /// <summary>
    /// 当前db
    /// </summary>
    public void CurrentRollbackTran()
    {
        Ado.RollbackTran();
    }

    /// <summary>
    /// 所有db
    /// </summary>
    public void BeginTran()
    {
        Context.AsTenant().BeginTran();
    }

    /// <summary>
    /// 所有db
    /// </summary>
    public void CommitTran()
    {
        Context.AsTenant().CommitTran();
    }

    /// <summary>
    /// 所有db
    /// </summary>
    public void RollbackTran()
    {
        Context.AsTenant().RollbackTran();
    }

    #endregion
}