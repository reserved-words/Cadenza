using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Cadenza.API.SqlLibrary.Services;

internal class DataAccess : IDataAccess
{
    private readonly IOptions<SqlLibrarySettings> _settings;

    public DataAccess(IOptions<SqlLibrarySettings> settings)
    {
        _settings = settings;
    }

    public async Task Execute(string storedProcedureName, object data)
    {
        using var connection = new SqlConnection(_settings.Value.ConnectionString);
        await connection.ExecuteAsync(storedProcedureName, data, commandType: CommandType.StoredProcedure);
    }

    public async Task Execute(string storedProcedureName, DynamicParameters parameters)
    {
        using var connection = new SqlConnection(_settings.Value.ConnectionString);
        await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<List<T>> Query<T>(string storedProcedureName, DynamicParameters parameters)
    {
        using var connection = new SqlConnection(_settings.Value.ConnectionString);
        var results = await connection.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
        return results.ToList();
    }

    public async Task<T> QuerySingle<T>(string storedProcedureName, DynamicParameters parameters = null)
    {
        using var connection = new SqlConnection(_settings.Value.ConnectionString);
        return await connection.QuerySingleAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
    }
}
