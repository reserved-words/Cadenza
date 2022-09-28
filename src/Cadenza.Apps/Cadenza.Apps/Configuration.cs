using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Apps;

public static class Configuration
{
    public static IServiceCollection ConfigureSettings<T>(this IServiceCollection services, IConfiguration config, string path) where T : class
    {
        var configSection = config.GetSection(path);
        services.Configure<T>(configSection);
        return services;
    }
}
