using Cadenza.Database.SqlLibrary;
using Cadenza.Database.SqlLibrary.Configuration;
using Cadenza.Local.SyncService;
using Cadenza.Common.LastFm;
using Cadenza.Common.LastFm.Settings;

var builder = Service.CreateBuilder(args, services =>
{
    var configuration = services.RegisterConfiguration();

    services
        .AddHttpClient()
        .AddHttpHelper();

    services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
         .CreateClient());

    services
        .AddUtilities()
        .AddSqlLibrary()
        .AddLastFm()
        .AddTransient<ISyncHttpHelper, SyncHttpHelper>()
        .AddTransient<ISourceRepository, LocalRepository>();

    services
        .AddTransient<IScheduledServiceFactory, ScheduledServiceFactory>()
        .AddKeyedTransient<IService, SyncHandler>("LibrarySync")
        .AddKeyedTransient<IService, UpdatesHandler>("LibraryUpdates")
        .AddKeyedTransient<IService, ScrobbleSyncer>("ScrobbleSync")
        .AddKeyedTransient<IService, Scrobbler>("Scrobbling")
        .AddKeyedTransient<IService, LovedTracksUpdater>("LovedTracks")
        .AddKeyedTransient<IService, NowPlayingUpdater>("NowPlaying");

    services
        .ConfigureSettings<LastFmApiSettings>(configuration, "LastFm")
        .ConfigureSettings<SqlLibrarySettings>(configuration, "SqlSettings")
        .ConfigureSettings<FrequencySecondsSettings>(configuration, "FrequencySeconds")
        .ConfigureSettings<LocalApiSettings>(configuration, "LocalApi");

    services.AddHostedService<Worker>();
});

builder.Build().Run();