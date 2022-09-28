using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Apps.WindowsService;

public static class Configuration
{
    public static IConfiguration RegisterJson(this IServiceCollection services)
    {
        var settingsPath = GetPath();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(settingsPath, false)
            .Build();

        services.AddSingleton(configuration);

        return configuration;
    }

    private static string GetPath()
    {
        return Environment.GetEnvironmentVariable("SETTINGS_PATH") ?? "appsettings.json";
    }
}
