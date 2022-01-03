namespace Cadenza.Local.API;

public static class Configuration
{
    public static WebApplicationBuilder RegisterConfiguration(this WebApplicationBuilder builder)
    {
        var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
            ?? "appsettings.json";

        builder.Configuration.AddJsonFile(settingsPath);

        builder.Services.Configure<LoggerOptions>(builder.Configuration.GetSection("Logging"));
        builder.Services.Configure<LibraryPaths>(builder.Configuration.GetSection("LibraryPaths"));
        builder.Services.Configure<CurrentlyPlaying>(builder.Configuration.GetSection("CurrentlyPlaying"));

        return builder;
    }
}
