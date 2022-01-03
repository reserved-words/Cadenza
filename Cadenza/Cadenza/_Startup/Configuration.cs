using Cadenza.Source.Spotify;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Cadenza;

public static class Configuration
{
    public static async Task<WebAssemblyHostBuilder> RegisterConfiguration(this WebAssemblyHostBuilder builder)
    {
        var settingsPath = "appsettings.json";

        var http = new System.Net.Http.HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        using var response = await http.GetAsync(settingsPath);
        using var stream = await response.Content.ReadAsStreamAsync();

        builder.Configuration.AddJsonStream(stream);

        builder.Services.ConfigureOptions<LoggerOptions>(builder.Configuration, "Logging");
        builder.Services.ConfigureOptions<SpotifyOverridesSettings>(builder.Configuration, "SpotifyOverrides");

        return builder;
    }
}
