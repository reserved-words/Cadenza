using Dapper;

namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IDatabaseAccess
{
    Task Execute(string storedProcedureName, DynamicParameters parameters);
}
