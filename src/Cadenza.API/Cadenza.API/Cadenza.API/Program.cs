global using Cadenza.API.Extensions;
global using Cadenza.API.Interfaces;
global using Cadenza.Apps;
global using Cadenza.Apps.API;
global using Cadenza.Common.DTO;
global using Cadenza.Common.DTO.Attributes;
global using Cadenza.Common.Enums;
global using Cadenza.Common.Model;
global using Cadenza.Common.Utilities;
global using Cadenza.Database.Interfaces;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;

using Cadenza.API.Services;
using Cadenza.Common;
using Cadenza.Database.SqlLibrary;
using Cadenza.Database.SqlLibrary.Configuration;
using Cadenza.Common.LastFm;
using Cadenza.Common.LastFm.Settings;

const string AuthConfigSectionName = "MainApiAuthentication";

var builder = API.CreateBuilder(AuthConfigSectionName, (IServiceCollection services, IConfiguration configuration) =>
{
    services
        .AddSqlLibrary()
        .AddLastFm()
        .AddUtilities()
        .AddHttpHelper();

    services
        .AddTransient<IShuffler, Shuffler>()
        .AddTransient<IUpdateService, UpdateService>();

    services
        .ConfigureSettings<LastFmApiSettings>(configuration, "LastFm")
        .ConfigureSettings<SqlLibrarySettings>(configuration, "SqlSettings");
}, JsonSerialization.SetOptions);

var app = API.CreateApp(builder, AuthConfigSectionName);

app.Run();