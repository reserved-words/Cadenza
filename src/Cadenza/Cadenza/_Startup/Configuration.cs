using Cadenza.Core.Player;
using Cadenza.Source.Local;
using Cadenza.Utilities;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Cadenza;

public static class Configuration
{
    public static async Task<WebAssemblyHostBuilder> RegisterConfiguration(this WebAssemblyHostBuilder builder)
    {
        var settingsPath = "appsettings.json";

        var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        using var response = await http.GetAsync(settingsPath);
        using var stream = await response.Content.ReadAsStreamAsync();

        builder.Configuration.AddJsonStream(stream);

        builder.Services.ConfigureLogger(builder.Configuration, "Logging");
//        builder.Services.ConfigureSpotifyOverrides(builder.Configuration, "SpotifyOverrides");
        builder.Services.ConfigureCoreAPI(builder.Configuration, "PlayerApi");

        return builder;
    }
}
