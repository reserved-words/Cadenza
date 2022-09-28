using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Cadenza._Startup;

public static class Configuration
{
    public static async Task<WebAssemblyHostBuilder> RegisterConfiguration(this WebAssemblyHostBuilder builder)
    {
        var settingsPath = "appsettings.json";

        var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        using var response = await http.GetAsync(settingsPath);
        using var stream = await response.Content.ReadAsStreamAsync();

        builder.Configuration.AddJsonStream(stream);

        return builder;
    }
}
