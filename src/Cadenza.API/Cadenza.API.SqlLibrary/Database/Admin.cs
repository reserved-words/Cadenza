namespace Cadenza.Database.SqlLibrary.Database;

internal class Admin : IAdmin
{
    private ISqlAccess _sql;

    public Admin(ISqlAccess sql)
    {
        _sql = sql;
    }

    public async Task<List<GroupingDTO>> GetGroupings()
    {
        // should be using Data model not DTO here
        return await _sql.Query<GroupingDTO>("[Admin].[GetGroupings]");
    }


}
