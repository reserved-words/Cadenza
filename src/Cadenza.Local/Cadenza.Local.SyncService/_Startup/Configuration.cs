namespace Cadenza.Local.SyncService._Startup;

public static class Configuration
{
    public static IServiceCollection RegisterConfiguration(this IServiceCollection services)
    {
        var configuration = services.RegisterJson();

        return services
            .ConfigureSettings<ServiceSettings>(configuration, "ServiceSettings")
            .ConfigureSettings<CurrentlyPlayingSettings>(configuration, "CurrentlyPlaying");
    }
}
