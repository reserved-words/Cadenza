using Cadenza.API.Core;
using Cadenza.API.LastFM;
using Cadenza.Utilities;

namespace Cadenza.API._Startup;

public static class Dependencies
{
    public static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddLastFM()
            .AddCoreServices()
            .AddLogger();

        return builder;
    }
}
