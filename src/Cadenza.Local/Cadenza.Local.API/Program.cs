global using Cadenza.Common.Domain.Model.Sync;
global using Cadenza.Common.Domain.Model.Updates;
global using Cadenza.Common.Utilities;
global using Cadenza.Local.API.Common.Controllers;
global using Cadenza.Local.API.Core;
global using Cadenza.Local.API.Core.Settings;
global using Cadenza.Local.API.Files;
global using Microsoft.AspNetCore.Mvc;

using Cadenza.Apps;
using Cadenza.Apps.API;

var builder = API.CreateBuilder("LocalApiAuthentication", (IServiceCollection services, IConfiguration configuration) =>
{
    services
        .AddUtilities()
        .AddMusicService()
        .AddCoreServices();

    services
        .ConfigureSettings<MusicLibrarySettings>(configuration, "MusicLibrary")
        .ConfigureSettings<CurrentlyPlayingSettings>(configuration, "CurrentlyPlaying");
});

var app = API.CreateApp(builder);

app.Run();