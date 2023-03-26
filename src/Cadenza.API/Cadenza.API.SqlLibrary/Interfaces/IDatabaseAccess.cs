using Dapper;

namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IDatabaseAccess
{
    Task Execute(string storedProcedureName, DynamicParameters parameters);
    Task<List<T>> Query<T>(string storedProcedureName, DynamicParameters parameters);
}
