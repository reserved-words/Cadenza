using Cadenza.Database.SqlLibrary.Configuration;
using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Cadenza.Database.SqlLibrary.Services;

internal class SqlAccess : ISqlAccess
{
    private readonly string _schemaName;
    private readonly IOptions<SqlLibrarySettings> _settings;

    public SqlAccess(IOptions<SqlLibrarySettings> settings, string schemaName)
    {
        _schemaName = schemaName;
        _settings = settings;
    }

    public async Task Execute(object data, [CallerMemberName] string storedProcedureName = null)
    {
        using var connection = new SqlConnection(_settings.Value.ConnectionString);
        await connection.ExecuteAsync(StoredProcedure(storedProcedureName), data, commandType: CommandType.StoredProcedure);
    }

    public async Task Execute(DynamicParameters parameters, [CallerMemberName] string storedProcedureName = null)
    {
        using var connection = new SqlConnection(_settings.Value.ConnectionString);
        await connection.ExecuteAsync(StoredProcedure(storedProcedureName), parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<List<T>> Query<T>(object parameters, [CallerMemberName] string storedProcedureName = null)
    {
        using var connection = new SqlConnection(_settings.Value.ConnectionString);
        var qualifiedStoredProcedureName = StoredProcedure(storedProcedureName);
        var results = await connection.QueryAsync<T>(qualifiedStoredProcedureName, parameters, commandType: CommandType.StoredProcedure);
        return results.ToList();
    }

    public async Task<List<T>> Query<T>(DynamicParameters parameters, [CallerMemberName] string storedProcedureName = null)
    {
        using var connection = new SqlConnection(_settings.Value.ConnectionString);
        var qualifiedStoredProcedureName = StoredProcedure(storedProcedureName);
        var results = await connection.QueryAsync<T>(qualifiedStoredProcedureName, parameters, commandType: CommandType.StoredProcedure);
        return results.ToList();
    }

    public async Task<T> QuerySingle<T>(object parameters, [CallerMemberName] string storedProcedureName = null)
    {
        using var connection = new SqlConnection(_settings.Value.ConnectionString);
        return await connection.QuerySingleAsync<T>(StoredProcedure(storedProcedureName), parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<T> QuerySingle<T>(DynamicParameters parameters, [CallerMemberName] string storedProcedureName = null)
    {
        using var connection = new SqlConnection(_settings.Value.ConnectionString);
        return await connection.QuerySingleAsync<T>(StoredProcedure(storedProcedureName), parameters, commandType: CommandType.StoredProcedure);
    }

    private string StoredProcedure(string storedProcedureName)
    {
        return $"[{_schemaName}].[{storedProcedureName}]";
    }
}
