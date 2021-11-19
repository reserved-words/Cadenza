using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace Cadenza;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");

        var http = new System.Net.Http.HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        builder.Services
            .AddTransient(sp => http)
            .AddMudServices()
            .AddAppServices()
            .AddUtilities()
            .AddTimers()
            .AddLastFm()
            .AddSpotify()
            .AddAzure()
            .AddLocalLibrary()
            .AddLibraries()
            .AddPlayers()
            .AddSourceFactories()
            .AddSingletons();

        builder.Services.AddTransient<IPlayerApiUrl, PlayerApiConfig>();

        var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
            ?? "appsettings.json";

        using var response = await http.GetAsync(settingsPath);
        using var stream = await response.Content.ReadAsStreamAsync();

        builder.Configuration.AddJsonStream(stream);

        await builder.Build().RunAsync();
    }
}
