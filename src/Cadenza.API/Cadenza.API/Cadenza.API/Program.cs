global using Microsoft.AspNetCore.Mvc;

global using Cadenza.API.Interfaces.Controllers;
global using Cadenza.API.Core;
global using Cadenza.API.Database;
global using Cadenza.API.LastFM;

global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Model;
global using Cadenza.Common.Domain.Model.Album;
global using Cadenza.Common.Domain.Model.Artist;
global using Cadenza.Common.Domain.Model.History;
global using Cadenza.Common.Domain.Model.LastFm;
global using Cadenza.Common.Domain.Model.Track;
global using Cadenza.Common.Domain.Model.Updates;

global using Cadenza.Common.Utilities;

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