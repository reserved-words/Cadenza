var builder = Service.CreateBuilder(args, services =>
{
    var configuration = services.RegisterConfiguration();

    services
        .AddHttpClient();

    services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
         .CreateClient());

    services
        .AddUtilities()
        .AddSingleton<IApiTokenCache, ApiTokenCache>()
        .AddTransient<IApiTokenFetcher, ApiTokenFetcher>()
        .AddTransient<ISyncHttpHelper, SyncHttpHelper>()
        .AddTransient<IDatabaseRepository, DatabaseRepository>()
        .AddTransient<ISourceRepository, LocalRepository>()
        .AddTransient<IService, RemovalRequestsHandler>()
        .AddTransient<IService, RemovedTracksHandler>()
        .AddTransient<IService, AddedTracksHandler>()
        .AddTransient<IService, UpdateRequestsHandler>();

    services
        .ConfigureSettings<ServiceSettings>(configuration, "ServiceSettings")
        .ConfigureSettings<DatabaseApiSettings>(configuration, "DatabaseApi")
        .ConfigureSettings<LocalApiSettings>(configuration, "LocalApi")
        .ConfigureSettings<AuthSettings>(configuration, "Authentication");

});

builder.Build().Run();