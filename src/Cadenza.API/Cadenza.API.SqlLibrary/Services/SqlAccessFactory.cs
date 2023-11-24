using Cadenza.Database.SqlLibrary.Configuration;
using Microsoft.Extensions.Options;

namespace Cadenza.Database.SqlLibrary.Services;

internal class SqlAccessFactory : ISqlAccessFactory
{
    private readonly IOptions<SqlLibrarySettings> _settings;

    public SqlAccessFactory(IOptions<SqlLibrarySettings> settings)
    {
        _settings = settings;
    }

    public ISqlAccess Create(string schemaName)
    {
        return new SqlAccess(_settings, schemaName);
    }
}
