using Cadenza.Common.Interfaces.Utilities;

var builder = Service.CreateBuilder(args, services =>
{
    var configuration = services.RegisterConfiguration();

    services
        .AddHttpClient("MainApi", client => client.BaseAddress = new Uri(configuration["DatabaseApi:BaseUrl"]));

    services
        .AddHttpClient("LocalApi", client => client.BaseAddress = new Uri(configuration["LocalApi:BaseUrl"]));
    
    services
        .AddUtilities()
        .AddTransient<IDatabaseRepository, DatabaseRepository>()
        .AddTransient<ISourceRepository, LocalRepository>()
        .AddTransient<IService, RemovalRequestsHandler>()
        .AddTransient<IService, RemovedTracksHandler>()
        .AddTransient<IService, AddedTracksHandler>()
        .AddTransient<IService, UpdateRequestsHandler>();

    services
        .AddTransient<IMainApiHttpHelper, MainApiHttpHelper>()
        .AddTransient<ILocalHttpHelper, LocalApiHttpHelper>();

    services
        .ConfigureSettings<ServiceSettings>(configuration, "ServiceSettings")
        .ConfigureSettings<DatabaseApiSettings>(configuration, "DatabaseApi")
        .ConfigureSettings<LocalApiSettings>(configuration, "LocalApi");

});

builder.Build().Run();