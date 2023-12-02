global using Cadenza.Apps;
global using Cadenza.Apps.API;
global using Cadenza.Common.DTO;
global using Cadenza.Common.Enums;
global using Cadenza.Common.Utilities;
global using Microsoft.AspNetCore.Mvc;
global using Cadenza.Database.Interfaces;
using Cadenza.Common;
using Cadenza.Database.SqlLibrary;
using Cadenza.API.Services;
using Cadenza.Database.SqlLibrary.Configuration;
using Cadenza.Common.LastFm;
using Cadenza.Common.LastFm.Settings;
using Cadenza.API.Interfaces;

const string AuthConfigSectionName = "MainApiAuthentication";

var builder = API.CreateBuilder(AuthConfigSectionName, (IServiceCollection services, IConfiguration configuration) =>
{
    services
        .AddSqlLibrary()
        .AddLastFm()
        .AddUtilities()
        .AddHttpHelper();

    services
        .AddTransient<IUpdateService, UpdateService>();

    services
        .ConfigureSettings<LastFmApiSettings>(configuration, "LastFm")
        .ConfigureSettings<SqlLibrarySettings>(configuration, "SqlSettings");
}, JsonSerialization.SetOptions);

var app = API.CreateApp(builder, AuthConfigSectionName);

app.Run();