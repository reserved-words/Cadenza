global using Cadenza.API.Cache;
global using Cadenza.API.Core;
global using Cadenza.API.Interfaces.Controllers;
global using Cadenza.API.LastFM;
global using Cadenza.API.LastFM.Settings;
global using Cadenza.Apps;
global using Cadenza.Apps.API;
global using Cadenza.Common.DTO;
global using Cadenza.Common.Enums;
global using Cadenza.Common.Utilities;
global using Microsoft.AspNetCore.Mvc;
using Cadenza.API.SqlLibrary;
using Cadenza.Common;

const string AuthConfigSectionName = "MainApiAuthentication";

var builder = API.CreateBuilder(AuthConfigSectionName, (IServiceCollection services, IConfiguration configuration) =>
{
    services
        .AddCache()
        .AddCoreServices()
        .AddSqlLibrary()
        .AddLastFM()
        .AddUtilities()
        .AddHttpHelper();

    services
        .ConfigureSettings<LastFmApiSettings>(configuration, "LastFm")
        .ConfigureSettings<SqlLibrarySettings>(configuration, "SqlSettings");
}, JsonSerialization.SetOptions);

var app = API.CreateApp(builder, AuthConfigSectionName);

app.Run();