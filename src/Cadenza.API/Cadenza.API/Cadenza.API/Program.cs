global using Cadenza.API.Cache;
global using Cadenza.API.LastFM;
global using Cadenza.API.LastFM.Settings;
global using Cadenza.Apps;
global using Cadenza.Apps.API;
global using Cadenza.Common.DTO;
global using Cadenza.Common.Enums;
global using Cadenza.Common.Utilities;
global using Microsoft.AspNetCore.Mvc;
global using Cadenza.Database.Interfaces;
using Cadenza.Common;
using Cadenza.Database.SqlLibrary;
using Cadenza.API.Interfaces.Services;
using Cadenza.API.Services;
using Cadenza.Database.SqlLibrary.Configuration;

const string AuthConfigSectionName = "MainApiAuthentication";

var builder = API.CreateBuilder(AuthConfigSectionName, (IServiceCollection services, IConfiguration configuration) =>
{
    services
        .AddCache()
        .AddSqlLibrary()
        .AddLastFM()
        .AddUtilities()
        .AddHttpHelper();

    services
        .AddTransient<IUpdateService, UpdateService>()
        .AddTransient<ICachePopulater, CachePopulater>();

    services
        .ConfigureSettings<LastFmApiSettings>(configuration, "LastFm")
        .ConfigureSettings<SqlLibrarySettings>(configuration, "SqlSettings");
}, JsonSerialization.SetOptions);

var app = API.CreateApp(builder, AuthConfigSectionName);

app.Run();