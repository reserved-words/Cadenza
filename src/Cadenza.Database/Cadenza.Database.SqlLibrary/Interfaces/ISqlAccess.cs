using Dapper;
using System.Runtime.CompilerServices;

namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface ISqlAccess
{
    Task Execute(object parameters, [CallerMemberName] string storedProcedureName = null);
    Task Execute(DynamicParameters parameters, [CallerMemberName] string storedProcedureName = null);
    Task<List<T>> Query<T>(object parameters, [CallerMemberName] string storedProcedureName = null);
    Task<List<T>> Query<T>(DynamicParameters parameters, [CallerMemberName] string storedProcedureName = null);
    Task<T> QuerySingle<T>(object parameters, [CallerMemberName] string storedProcedureName = null);
    Task<T> QuerySingle<T>(DynamicParameters parameters, [CallerMemberName] string storedProcedureName = null);
}
