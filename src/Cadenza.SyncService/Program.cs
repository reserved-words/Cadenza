using Cadenza.Database.SqlLibrary;
using Cadenza.Database.SqlLibrary.Configuration;

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
        .AddTransient<IService, UpdateRequestsHandler>()
        .AddTransient<IService, SyncHandler>();

    services
        .ConfigureSettings<SqlLibrarySettings>(configuration, "SqlSettings")
        .ConfigureSettings<ServiceSettings>(configuration, "ServiceSettings")
        .ConfigureSettings<LocalApiSettings>(configuration, "LocalApi")
        .ConfigureSettings<AuthSettings>(configuration, "SvcAuthentication");

});

builder.Build().Run();