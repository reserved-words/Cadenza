global using Cadenza.Common.Domain.Model.Sync;
global using Cadenza.Common.Utilities;
global using Cadenza.Local.API.Common.Controllers;
global using Cadenza.Local.API.Core;
global using Cadenza.Local.API.Core.Settings;
global using Cadenza.Local.API.Files;
global using Cadenza.Common.DTO;
global using Cadenza.Common.Utilities.Interfaces;
global using Microsoft.AspNetCore.Mvc;

using Cadenza.Apps;
using Cadenza.Apps.API;

const string AuthConfigSectionName = "LocalApiAuthentication";

var builder = API.CreateBuilder(AuthConfigSectionName, (IServiceCollection services, IConfiguration configuration) =>
{
    services
        .AddUtilities()
        .AddMusicService()
        .AddCoreServices();

    services
        .ConfigureSettings<MusicLibrarySettings>(configuration, "MusicLibrary")
        .ConfigureSettings<CurrentlyPlayingSettings>(configuration, "CurrentlyPlaying");
});

var app = API.CreateApp(builder, AuthConfigSectionName);

app.Run();