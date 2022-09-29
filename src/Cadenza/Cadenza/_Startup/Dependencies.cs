namespace Cadenza._Startup;

public static class Dependencies
{
    public static WebAssemblyHostBuilder RegisterDependencies(this WebAssemblyHostBuilder builder)
    {
        var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        builder.Services
            .AddSingleton<SearchRepositoryCache>()
            .AddTransient<ISearchSyncService, SearchSyncService>();

        builder.Services
            .AddSingleton<ConnectorService>()
            .AddTransient<IConnectorConsumer>(sp => sp.GetRequiredService<ConnectorService>())
            .AddTransient<IConnectorController>(sp => sp.GetRequiredService<ConnectorService>())
            .AddTransient<ILongRunningTaskService, LongRunningTaskService>()
            .AddStartupServices()
            .AddInteropServices()
            .AddUtilities()
            .AddHttpHelper(sp => http)
            .AddAppServices()
            .AddComponents()
            .AddTimers()
            .AddAPIWrapper()
            .AddLastFm(builder.Configuration, "LastFmApi")
            .AddSources(builder.Configuration)
            .AddDatabase(builder.Configuration, "DatabaseApi")
            .AddTransient<IStoreGetter, Store>()
            .AddTransient<IStoreSetter, Store>();

        builder.Services
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IItemPlayer, ItemPlayer>()
            .AddTransient<IItemViewer, ItemViewer>();

        builder.Services
            .AddTransient<IPlayer, CorePlayer>()
            .AddTransient<IUtilityPlayer, TimingPlayer>()
            .AddTransient<IUtilityPlayer, TrackingPlayer>();

        return builder;
    }

    private static IServiceCollection AddInteropServices(this IServiceCollection services)
    {
        return services.AddTransient<INavigation, NavigationInterop>()
            .AddTransient<IStore, StoreInterop>();
    }

    private static IServiceCollection AddStartupServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IStartupConnectService, StartupConnectService>();
    }

    private static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<AppService>()
            .AddSingleton<ItemUpdatesHandler>()
            .AddTransient<IAppConsumer>(sp => sp.GetRequiredService<AppService>())
            .AddTransient<IAppController>(sp => sp.GetRequiredService<AppService>())
            .AddTransient<IUpdatesConsumer>(sp => sp.GetRequiredService<ItemUpdatesHandler>())
            .AddTransient<IUpdatesController>(sp => sp.GetRequiredService<ItemUpdatesHandler>());
    }

    private static IServiceCollection AddTimers(this IServiceCollection services)
    {
        return services
            .AddSingleton<TrackTimer>()
            .AddTransient<ITrackTimerController>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackProgressedConsumer>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackFinishedConsumer>(sp => sp.GetRequiredService<TrackTimer>());
    }

    private static IServiceCollection AddSources(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddLocalSource<HtmlPlayer>(config, "LocalApi")
            .AddTransient<IArtworkFetcher, CoreArtworkFetcher>();
    }

    public static IServiceCollection AddAPIWrapper(this IServiceCollection services)
    {
        return services
            .AddTransient<IUrl, Url>();
    }
}