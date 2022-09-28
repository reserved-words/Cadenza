namespace Cadenza.Local.API._Startup;

public static class Configuration
{
    public static WebApplicationBuilder RegisterConfiguration(this WebApplicationBuilder builder)
    {
        var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
            ?? "appsettings.json";

        builder.Configuration.AddJsonFile(settingsPath);

        builder.Services
            .ConfigureMusicLocation(builder.Configuration, "MusicLibrary")
            .ConfigurePlayLocation(builder.Configuration, "CurrentlyPlaying");

        return builder;
    }

    private static IServiceCollection ConfigurePlayLocation(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<CurrentlyPlayingSettings>(section);
    }
}
