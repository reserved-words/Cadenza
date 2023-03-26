using Cadenza.API.SqlLibrary.Interfaces;
using Dapper;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Cadenza.API.SqlLibrary.Services;

internal class DatabaseAccess : IDatabaseAccess
{
    private readonly IOptions<SqlLibrarySettings> _settings;

    public DatabaseAccess(IOptions<SqlLibrarySettings> settings)
    {
        _settings = settings;
    }

    public async Task Execute(string storedProcedureName, DynamicParameters parameters)
    {
        using var connection = new SqlConnection(_settings.Value.ConnectionString);
        await connection.ExecuteAsync(storedProcedureName, parameters);
    }
}
