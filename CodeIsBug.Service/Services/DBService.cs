using CodeIsBug.Repository.Repository;

namespace CodeIsBug.Service.Services;

public  class DbService(DBRepository dbRepository)
{
    public void SyncDb()
    {
        dbRepository.SyncDB();

    }
}
