using Cadenza.Database.SqlLibrary;
using Cadenza.Database.SqlLibrary.Configuration;
using Cadenza.Local.SyncService;
using Cadenza.SyncService.Updaters.Interfaces;

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
        .AddSingleton<IApiTokenCache, ApiTokenCache>()
        .AddTransient<IApiTokenFetcher, ApiTokenFetcher>()
        .AddTransient<ISyncHttpHelper, SyncHttpHelper>()
        .AddTransient<ISourceRepository, LocalRepository>()
        .AddTransient<IScheduledServiceFactory, ScheduledServiceFactory>()
        .AddTransient<IUpdatesHandler, UpdatesHandler>()
        .AddTransient<ISyncHandler, SyncHandler>();

    services
        .ConfigureSettings<SqlLibrarySettings>(configuration, "SqlSettings")
        .ConfigureSettings<ServiceSettings>(configuration, "ServiceSettings")
        .ConfigureSettings<LocalApiSettings>(configuration, "LocalApi")
        .ConfigureSettings<AuthSettings>(configuration, "SvcAuthentication");

    services.AddHostedService<Worker>();
});

builder.Build().Run();