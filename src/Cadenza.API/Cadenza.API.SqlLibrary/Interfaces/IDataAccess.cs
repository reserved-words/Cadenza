using Dapper;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface IDataAccess
{
    Task Execute(string storedProcedureName, object data);
    Task Execute(string storedProcedureName, DynamicParameters parameters = null);
    Task<List<T>> Query<T>(string storedProcedureName, DynamicParameters parameters = null);
    Task<T> QuerySingle<T>(string storedProcedureName, DynamicParameters parameters = null);
}
