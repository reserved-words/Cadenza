using Cadenza.Database.SqlLibrary.Database.Interfaces;
using Dapper;
using System.Data;

namespace Cadenza.Database.SqlLibrary.Database;

internal class Admin : IAdmin
{
    private readonly ISqlAccess _sql;

    public Admin(ISqlAccessFactory sql)
    {
        _sql = sql.Create(nameof(Admin));
    }

    public async Task<bool> HasLastFmSessionKey(string username)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Username", username);
        parameters.Add("@HasSessionKey", dbType: DbType.Boolean, direction: ParameterDirection.Output);
        await _sql.Execute(parameters);
        return parameters.Get<bool>("@HasSessionKey");
    }

    public async Task SaveLastFmSessionKey(string username, string lastFmUsername, string lastFmSessionKey)
    {
        await _sql.Execute(new { Username = username, LastFmUsername = lastFmUsername, LastFmSessionKey = lastFmSessionKey });
    }
}
