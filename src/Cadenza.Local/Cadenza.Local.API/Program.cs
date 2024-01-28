global using Cadenza.Common.DTO;
global using Cadenza.Common.Utilities;
global using Cadenza.Common.Utilities.Interfaces;
global using Cadenza.Local.API.Common.Controllers;
global using Cadenza.Local.API.Core;
global using Cadenza.Local.API.Core.Settings;
global using Cadenza.Local.API.Files;
global using Microsoft.AspNetCore.Mvc;
using Cadenza.Apps;
using Cadenza.Apps.API;
using Cadenza.Common;
using Cadenza.Local.FileAccess;

var builder = API.CreateBuilder((IServiceCollection services, IConfiguration configuration) =>
{
    services
        .AddUtilities()
        .AddMusicService()
        .AddCoreServices()
        .AddFileAccess();

    services
        .ConfigureSettings<MusicLibrarySettings>(configuration, "MusicLibrary")
        .ConfigureSettings<CurrentlyPlayingSettings>(configuration, "CurrentlyPlaying");
}, JsonSerialization.SetOptions);

var app = API.CreateApp(builder);

app.Run();