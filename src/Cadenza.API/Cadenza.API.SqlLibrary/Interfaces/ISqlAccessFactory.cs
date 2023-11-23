namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface ISqlAccessFactory
{
    ISqlAccess Create(string schemaName);
}
