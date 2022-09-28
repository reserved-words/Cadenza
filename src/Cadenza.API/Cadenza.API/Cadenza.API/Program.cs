global using Microsoft.AspNetCore.Mvc;

global using Cadenza.API.Interfaces.Controllers;
global using Cadenza.API.Core;
global using Cadenza.API.Database;
global using Cadenza.API.LastFM;

global using Cadenza.Domain.Enums;
global using Cadenza.Domain.Model;
global using Cadenza.Domain.Model.Album;
global using Cadenza.Domain.Model.Artist;
global using Cadenza.Domain.Model.History;
global using Cadenza.Domain.Model.LastFm;
global using Cadenza.Domain.Model.Track;
global using Cadenza.Domain.Model.Updates;

global using Cadenza.Utilities;

using Cadenza.API.LastFM.Settings;
using Cadenza.Apps;
using Cadenza.Apps.API;

var builder = API.CreateBuilder(args, (IServiceCollection services, IConfiguration configuration) =>
{
    services
        .AddCoreServices()
        .AddJsonLibrary()
        .AddLastFM()
        .AddUtilities()
        .AddHttpHelper(sp => new HttpClient());

    services
        .ConfigureSettings<LastFmApiSettings>(configuration, "LastFm")
        .ConfigureSettings<LibraryPathSettings>(configuration, "LibraryPaths");
});

var app = API.CreateApp(builder);

app.Run();