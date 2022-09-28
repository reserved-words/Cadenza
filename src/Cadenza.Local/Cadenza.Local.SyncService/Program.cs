
var builder = Service.CreateBuilder(args, services =>
{
    var configuration = services.RegisterConfiguration();

    services
        .AddUtilities()
        .AddTransient<IService, PlayedFilesService>();

    services
        .ConfigureSettings<ServiceSettings>(configuration, "ServiceSettings")
        .ConfigureSettings<CurrentlyPlayingSettings>(configuration, "CurrentlyPlaying");
});

builder.Build().Run();