namespace Cadenza.Database.SqlLibrary.Database;

internal class Admin : IAdmin
{
    private readonly ISqlAccess _sql;

    public Admin(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Admin));
    }

    public async Task<List<GroupingDTO>> GetGroupings()
    {
        // should be using Data model not DTO here
        return await _sql.Query<GroupingDTO>(null);
    }


}
