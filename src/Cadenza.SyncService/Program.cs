
var builder = Service.CreateBuilder(args, services =>
{
    var configuration = services.RegisterConfiguration();

    services
        .AddUtilities()
        .AddHttpHelper(sp => new HttpClient())
        .AddTransient<IDatabaseRepository, DatabaseRepository>()
        .AddTransient<ISourceRepository, LocalRepository>()
        .AddTransient<IService, AddedTracksHandler>()
        .AddTransient<IService, RemovedTracksHandler>()
        .AddTransient<IService, UpdatesHandler>();

    services
        .ConfigureSettings<ServiceSettings>(configuration, "ServiceSettings")
        .ConfigureSettings<DatabaseApiSettings>(configuration, "DatabaseApi")
        .ConfigureSettings<LocalApiSettings>(configuration, "LocalApi");

});

builder.Build().Run();