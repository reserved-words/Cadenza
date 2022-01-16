namespace Cadenza.Local.API;

public static class Configuration
{
    public static WebApplicationBuilder RegisterConfiguration(this WebApplicationBuilder builder)
    {
        var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
            ?? "appsettings.json";

        builder.Configuration.AddJsonFile(settingsPath);

        builder.Services
            .ConfigureLogger(builder.Configuration, "Logging")
            .ConfigureLibraryLocation(builder.Configuration, "LibraryPaths")
            .ConfigurePlayLocation(builder.Configuration, "CurrentlyPlaying");

        return builder;
    }
}
