global using Cadenza.Common.Domain.Model.Track;
global using Cadenza.Common.Domain.Model.Updates;
global using Cadenza.Common.Utilities;
global using Cadenza.Local.API.Common.Controllers;
global using Cadenza.Local.API.Core;
global using Cadenza.Local.API.Core.Settings;
global using Cadenza.Local.API.Files;
global using Microsoft.AspNetCore.Mvc;
using Cadenza.Apps;
using Cadenza.Apps.API;

var builder = API.CreateBuilder(args, (IServiceCollection services, IConfiguration configuration) =>
{
    services
        .AddUtilities()
        .AddArtworkService()
        .AddMusicService()
        .AddCoreServices();

    services
        .ConfigureSettings<MusicLibrarySettings>(configuration, "MusicLibrary")
        .ConfigureSettings<CurrentlyPlayingSettings>(configuration, "CurrentlyPlaying");
});

var app = API.CreateApp(builder);

app.Run();