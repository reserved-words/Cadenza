using Cadenza.Azure;
using Cadenza.LastFM;

namespace Cadenza.API;

public static class Configuration
{
    public static async Task<WebApplicationBuilder> RegisterConfiguration(this WebApplicationBuilder builder)
    {
        var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
            ?? "appsettings.json";

        builder.Configuration.AddJsonFile(settingsPath);

        builder.Services.Configure<LastFmSettings>(builder.Configuration.GetSection("LastFm"));
        builder.Services.Configure<AzureSettings>(builder.Configuration.GetSection("Azure"));
        builder.Services.Configure<Cadenza.API.Spotify.Settings>(builder.Configuration.GetSection("Spotify"));

        return builder;
    }
}
