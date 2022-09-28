using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.WindowsService;

public static class Configuration
{
    public static IConfiguration RegisterJson(this IServiceCollection services)
    {
        var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
                ?? "appsettings.json";

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(settingsPath, false)
            .Build();

        services.AddSingleton(configuration);

        return configuration;
    }

    public static IServiceCollection ConfigureSettings<T>(this IServiceCollection services, IConfiguration config, string path) where T : class
    {
        var configSection = config.GetSection(path);
        services.Configure<T>(configSection);
        return services;
    }
}
