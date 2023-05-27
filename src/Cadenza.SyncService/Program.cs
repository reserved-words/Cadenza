var builder = Service.CreateBuilder(args, services =>
{
    var configuration = services.RegisterConfiguration();

    services
        .AddHttpClient();

    services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
         .CreateClient());

    services
        .AddUtilities()
        .AddTransient<IDatabaseRepository, DatabaseRepository>()
        .AddTransient<ISourceRepository, LocalRepository>()
        .AddTransient<IService, RemovalRequestsHandler>()
        .AddTransient<IService, RemovedTracksHandler>()
        .AddTransient<IService, AddedTracksHandler>()
        .AddTransient<IService, UpdateRequestsHandler>();

    services
        .AddTransient<ISyncHttpHelper, SyncHttpHelper>();

    services
        .ConfigureSettings<ServiceSettings>(configuration, "ServiceSettings")
        .ConfigureSettings<DatabaseApiSettings>(configuration, "DatabaseApi")
        .ConfigureSettings<LocalApiSettings>(configuration, "LocalApi")
        .ConfigureSettings<AuthSettings>(configuration, "Authentication");

});

builder.Build().Run();