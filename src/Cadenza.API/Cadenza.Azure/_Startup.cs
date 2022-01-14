using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Azure;

public static class Startup
{
    public static IServiceCollection AddAzure(this IServiceCollection services)
    {
        return services.AddTransient<SpotifyOverridesService>();
    }

    public static IServiceCollection ConfigureAzure(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<AzureSettings>(section);
    }
}
