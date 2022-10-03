namespace Cadenza._Startup;

public static class Configuration
{
    public static async Task<WebAssemblyHostBuilder> RegisterConfiguration(this WebAssemblyHostBuilder builder)
    {
        await builder.RegisterJsonConfiguration();

        builder.Services
            .ConfigureSettings<LastFmApiSettings>(builder.Configuration, "LastFmApi")
            .ConfigureSettings<LocalApiSettings>(builder.Configuration, "LocalApi")
            .ConfigureSettings<DatabaseApiSettings>(builder.Configuration, "DatabaseApi");

        return builder;
    }

    private static async Task RegisterJsonConfiguration(this WebAssemblyHostBuilder builder)
    {
        var settingsPath = "appsettings.json";

        var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        using var response = await http.GetAsync(settingsPath);
        using var stream = await response.Content.ReadAsStreamAsync();

        builder.Configuration.AddJsonStream(stream);
    }
}
