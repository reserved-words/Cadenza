using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Admin;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Admin : IAdmin
{
    private readonly ISqlAccess _sql;

    public Admin(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Admin));
    }

    public async Task<List<GetGroupingsResult>> GetGroupings()
    {
        return await _sql.Query<GetGroupingsResult>(null);
    }
}
