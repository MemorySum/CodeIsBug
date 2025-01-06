namespace CodeIsBug.Repository.Repository;

/// <summary>
/// 
/// </summary>
public class UserRepository : BaseFreeSqlRepository<User>
{
    private readonly IFreeSql _freeSql1;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="freeSql"></param>
    public UserRepository(IFreeSql freeSql) : base(freeSql)
    {
        _freeSql1 = freeSql;
    }

    /// <summary>
    /// 登录查询数据库账号
    /// </summary>
    /// <param name="loginInputDto"></param>
    /// <returns></returns>
    public async Task<User> LoginAsync(LoginInputDto loginInputDto)
    {
        return await _freeSql1.Select<User>()
            .Where(a => a.UserName == loginInputDto.UserName && a.Password == loginInputDto.Password.Md5Hash())
            .FirstAsync();
    }
}