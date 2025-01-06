using CodeIsBug.Repository.Repository;

namespace CodeIsBug.Service.Services;

/// <summary>
/// DbService
/// </summary>
/// <param name="dbRepository"></param>
public  class DbService(DBRepository dbRepository)
{
    /// <summary>
    /// 
    /// </summary>
    public void SyncDb()
    {
        dbRepository.SyncDb();
    }

    /// <summary>
    /// 初始化种子数据
    /// </summary>
    public async Task InitDataAsync()
    {
       await dbRepository.InitDataAsync();
    }
}
