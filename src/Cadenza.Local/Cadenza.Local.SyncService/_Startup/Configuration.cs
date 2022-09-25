using Cadenza.Local.Common.Config;

namespace Cadenza.Local.SyncService._Startup;

public static class Configuration
{
    public static IServiceCollection RegisterConfiguration(this IServiceCollection services)
    {
        var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
            ?? "appsettings.json";

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(settingsPath, false)
            .Build();

        services.AddSingleton(configuration);

        services
            .ConfigurePlayLocation(configuration, "CurrentlyPlaying");

        return services;
    }

    private static IServiceCollection ConfigurePlayLocation(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<CurrentlyPlayingSettings>(section);
    }
}
