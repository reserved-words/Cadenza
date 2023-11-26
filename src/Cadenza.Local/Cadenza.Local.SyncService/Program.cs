using Cadenza.Local.SyncService;

var builder = Service.CreateBuilder(args, services =>
{
    var configuration = services.RegisterConfiguration();

    services
        .AddFileAccess()
        .AddTransient<IService, PlayedFilesService>();

    services
        .ConfigureSettings<ServiceSettings>(configuration, "ServiceSettings")
        .ConfigureSettings<CurrentlyPlayingSettings>(configuration, "CurrentlyPlaying");

    services.AddHostedService<Worker>();
});

builder.Build().Run();