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
        .AddTransient<IService, UpdateRequestsHandler>()
        .AddTransient<IService, SyncHandler>();

    services
        .ConfigureSettings<ServiceSettings>(configuration, "ServiceSettings")
        .ConfigureSettings<DatabaseApiSettings>(configuration, "DatabaseApi")
        .ConfigureSettings<LocalApiSettings>(configuration, "LocalApi")
        .ConfigureSettings<AuthSettings>(configuration, "SvcAuthentication");

});

builder.Build().Run();