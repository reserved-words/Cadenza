using Cadenza.Database;
using IndexedDB.Blazor;
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
            .AddLocalLibrary()
            .AddLibraries()
            .AddPlayers()
            .AddSourceFactories()
            .AddSingletons();

        builder.Services.AddTransient<IPlayerApiUrl, PlayerApiConfig>();

        builder.Services.AddSingleton<IIndexedDbFactory, IndexedDbFactory>();
        builder.Services.AddTransient<IMainRepository, MainRepository>();
        builder.Services.AddTransient<IArtistRepository, ArtistRepository>();
        builder.Services.AddTransient<ITrackRepositoryUpdater, Database.TrackRepository>();
        builder.Services.AddTransient<ITrackRepository, Player.TrackRepository>();
        builder.Services.AddTransient<IPlayTrackRepositoryUpdater, Database.PlayTrackRepository>();
        builder.Services.AddTransient<IPlayTrackRepository, Player.PlayTrackRepository>();

        builder.Services.AddTransient<IStartupSyncService, StartupSyncService>();

        var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
            ?? "appsettings.json";

        using var response = await http.GetAsync(settingsPath);
        using var stream = await response.Content.ReadAsStreamAsync();

        builder.Configuration.AddJsonStream(stream);

        await builder.Build().RunAsync();
    }
}
