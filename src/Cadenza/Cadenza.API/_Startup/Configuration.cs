namespace Cadenza.API;

public static class Configuration
{
    public static WebApplicationBuilder RegisterConfiguration(this WebApplicationBuilder builder)
    {
        var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
            ?? "appsettings.json";

        builder.Configuration
            .AddJsonFile(settingsPath);

        builder.Services
            .ConfigureAzure(builder.Configuration, "Azure")
            .ConfigureLastFM(builder.Configuration, "LastFm")
            .ConfigureSpotify(builder.Configuration, "Spotify");

        return builder;
    }
}
