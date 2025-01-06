using CodeIsBug.Common;
using CodeIsBug.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeIsBug.Api.Controller;

/// <summary>
/// 数据库控制器
/// </summary>
[ApiExplorerSettings(GroupName = "后台管理")]
public class DbController : ApiControllerBase
{
    private readonly DbService _dbService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dbService"></param>
    public DbController(DbService dbService)
    {
        this._dbService = dbService;
    }
    /// <summary>
    /// 同步数据库
    /// </summary>
    /// 
    [HttpGet]
    public void SyncDb()
    {
        _dbService.SyncDb();
    }

    
    /// <summary>
    /// 初始化种子数据
    /// </summary>
    [HttpGet]
    public async Task InitDataAsync()
    {
        await _dbService.InitDataAsync();
    }
}
