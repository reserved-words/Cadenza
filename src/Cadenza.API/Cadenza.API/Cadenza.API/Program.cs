global using Cadenza.API.Cache;
global using Cadenza.API.Core;
global using Cadenza.API.Interfaces.Controllers;
global using Cadenza.API.LastFM;
global using Cadenza.API.LastFM.Settings;
global using Cadenza.Apps;
global using Cadenza.Apps.API;
global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Utilities;
global using Microsoft.AspNetCore.Mvc;
global using Cadenza.Common.DTO;


using Cadenza.API.SqlLibrary;

const string AuthConfigSectionName = "MainApiAuthentication";

var builder = API.CreateBuilder(AuthConfigSectionName, (IServiceCollection services, IConfiguration configuration) =>
{
    services
        .AddCache()
        .AddCoreServices()
        .AddSqlLibrary()
        .AddLastFM()
        .AddUtilities()
        .AddDefaultHttpHelper();

    services
        .ConfigureSettings<LastFmApiSettings>(configuration, "LastFm")
        .ConfigureSettings<SqlLibrarySettings>(configuration, "SqlSettings");
});

var app = API.CreateApp(builder, AuthConfigSectionName);

app.Run();