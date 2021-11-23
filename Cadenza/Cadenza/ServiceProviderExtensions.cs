﻿using Cadenza._Config;
using Cadenza.Azure;
using Cadenza.Library;
using Cadenza.Source.Local;
using Cadenza.Source.Spotify;
using HttpClient = Cadenza.Common.HttpClient;

namespace Cadenza;

internal static class ServiceProviderExtensions
{
    public static IServiceCollection AddSingletons(this IServiceCollection services)
    {
        var spotifyCache = new Cache();
        var mainCache = new Cache();

        services
            .AddTransient<ICombinedSourceLibrary>(sp => new CombinedSourceLibrary(
                mainCache,
                sp.GetRequiredService<IMerger>(),
                new List<SourceLibrary>
                {
                        sp.GetRequiredService<LocalSourceLibrary>(),
                        sp.GetRequiredService<SpotifyLibrary>()
                }))
            .AddTransient<ICombinedSourceLibraryUpdater>(sp => new CombinedSourceLibraryUpdater(
                sp.GetUpdaters(),
                sp.GetRequiredService<IMerger>(),
                mainCache))
            .AddTransient<SpotifyLibrary>(sp =>
            {
                var merger = sp.GetRequiredService<IMerger>();
                var staticSources = new List<IStaticSource> {
                        sp.GetService<SpotifyOverrides>(),
                        sp.GetService<SpotifyApiLibrary>()
                };
                var combinedLibrary = new CombinedStaticLibrary(spotifyCache, merger, staticSources);
                return new SpotifyLibrary(combinedLibrary);
            })
            .AddTransient<SpotifyUpdater>(sp => new SpotifyUpdater(
                new LibraryUpdater(sp.GetRequiredService<IMerger>(), spotifyCache),
                sp.GetRequiredService<IOverridesService>()
                ));

        return services.AddSingleton<TrackTimer>()
            .AddSingleton<SpotifyPlayer>()
            .AddSingleton<IPlayer>(sp => new TrackingPlayer(
                sp.GetRequiredService<TimingPlayer>(),
                sp.GetRequiredService<IPlayTracker>(),
                sp.GetRequiredService<IViewModelLibrary>()))
            .AddSingleton<LocalPlayer>()
            .AddSingleton<NewLibrary>();
        ;
    }

    public static IServiceCollection AddAppServices(this IServiceCollection sc)
    {
        sc.AddSingleton<AppService>();
        sc.AddTransient<IAppConsumer>(sp => sp.GetRequiredService<AppService>());
        sc.AddTransient<IAppController>(sp => sp.GetRequiredService<AppService>());
        return sc;
    }

    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        return services
            .AddTransient<IDialogService, MudDialogService>()
            .AddTransient<IDateTime, CurrentDateTime>()
            .AddTransient<IHasher, Hasher>()
            .AddTransient<IHttpClient, HttpClient>()
            .AddTransient<IIdGenerator, IdGenerator>()
            .AddTransient<IMerger, Merger>()
            .AddTransient<INameComparer, NameComparer>()
            .AddTransient<INotificationService, MudNotificationService>()
            .AddTransient<IRandomGenerator, RandomGenerator>()
            .AddTransient<ISorter, Sorter>()
            .AddTransient<IStoreGetter, Store>()
            .AddTransient<IStoreSetter, Store>()
            .AddTransient<IPlaylistCreator, PlaylistCreator>()
            .AddTransient<IShuffler, Shuffler>()
            .AddTransient<ITimeSpanConverter, TimeSpanConverter>()
            .AddTransient<IValueMerger, ValueMerger>();
    }

    public static IServiceCollection AddTimers(this IServiceCollection services)
    {
        return services
            .AddTransient<ITrackTimerController>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackProgressedConsumer>(sp => sp.GetRequiredService<TrackTimer>())
            .AddTransient<ITrackFinishedConsumer>(sp => sp.GetRequiredService<TrackTimer>());
    }

    public static IServiceCollection AddLastFm(this IServiceCollection services)
    {
        return services.AddTransient<IPlayTracker, LastFmService>()
            .AddTransient<IFavouritesConsumer, LastFmService>()
            .AddTransient<IFavouritesController, LastFmService>();
    }

    public static IServiceCollection AddAzure(this IServiceCollection services)
    {
        return services.AddTransient<IAzureConfig, AzureConfig>()
            .AddTransient<IOverridesService, SpotifyOverridesService>();
    }

    public static IServiceCollection AddSpotify(this IServiceCollection services)
    {
        services.AddTransient<ISpotifyApiConfig, SpotifyConfig>()
            .AddTransient<ISpotifyApi, SpotifyApi>()
            .AddTransient<ISpotifyLibraryApi, SpotifyLibraryApi>()
            .AddTransient<ISpotifyPlayerApi, SpotifyPlayerApi>()
            .AddTransient<SpotifyOverridesService>();

        return services
            .AddTransient<SpotifyOverrides>()
            .AddTransient<SpotifyApiLibrary>();
    }

    public static IServiceCollection AddPlayers(this IServiceCollection services)
    {
        services.AddTransient<CorePlayer>();

        services.AddTransient<TimingPlayer>(sp => new TimingPlayer(
            sp.GetRequiredService<CorePlayer>(),
            sp.GetRequiredService<ITrackTimerController>()));


        return services;
    }

    public static IServiceCollection AddLocalLibrary(this IServiceCollection services)
    {
        return services
            .AddTransient<LocalLibraryUpdater>()
            .AddTransient<IAudioPlayer, HtmlPlayer>()
            .AddTransient<IFileUpdateQueue, LocalLibraryUpdater>()
            .AddTransient<ILocalApiConfig, LocalApiConfig>();
    }

    public static IServiceCollection AddLibraries(this IServiceCollection services)
    {
        return services
            .AddTransient<LocalLibrary>()
            .AddTransient<LocalSourceLibrary>()
            .AddTransient<IViewModelLibrary>(sp => sp.GetRequiredService<NewLibrary>())
            .AddTransient<ILibraryController>(sp => sp.GetRequiredService<NewLibrary>());
    }

    //public static IServiceCollection AddUpdaters(this IServiceCollection services)
    //{
    //    return services
    //        .AddTransient<ILibraryUpdater, LibraryUpdater>()
    //        .AddTransient<IUpdater, Updater>();
    //}

    public static IServiceCollection AddSourceFactories(this IServiceCollection services)
    {
        return services
            .AddTransient(sp => GetPlayers(sp))
            .AddTransient(sp => sp.GetUpdaters())
            .AddTransient(sp => GetOverriders(sp))
            .AddTransient(sp => GetSourceRepositories(sp));
    }

    private static Dictionary<LibrarySource, ISourceRepository> GetSourceRepositories(IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, ISourceRepository>
            {
                { LibrarySource.Local, sp.GetService<LocalLibrary>() },
                { LibrarySource.Spotify, sp.GetService<SpotifyLibrary>() }
            };
    }

    private static Dictionary<LibrarySource, IAudioPlayer> GetPlayers(IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, IAudioPlayer>
            {
                { LibrarySource.Local, sp.GetService<LocalPlayer>() },
                { LibrarySource.Spotify, sp.GetService<SpotifyPlayer>() }
            };
    }

    private static Dictionary<LibrarySource, ISourceLibraryUpdater> GetUpdaters(this IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, ISourceLibraryUpdater>
            {
                { LibrarySource.Local, sp.GetService<LocalLibraryUpdater>() },
                { LibrarySource.Spotify, sp.GetService<SpotifyUpdater>() }
            };
    }

    private static Dictionary<LibrarySource, IOverridesService> GetOverriders(IServiceProvider sp)
    {
        return new Dictionary<LibrarySource, IOverridesService>
            {
                { LibrarySource.Spotify, sp.GetService<SpotifyOverridesService>() }
            };
    }
}