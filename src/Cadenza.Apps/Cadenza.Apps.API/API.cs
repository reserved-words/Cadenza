﻿global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
using Cadenza.Common.Domain.JsonConverters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cadenza.Apps.API;

public static class API
{
    public static WebApplicationBuilder CreateBuilder(string authConfigSectionName, Action<IServiceCollection, IConfiguration> registerDependencies)
    {
        var builder = WebApplication.CreateBuilder(Array.Empty<string>());

        builder.RegisterConfiguration();

        builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(ctx.Configuration.LogFilePath(), rollingInterval: RollingInterval.Day));

        registerDependencies(builder.Services, builder.Configuration);

        builder.Services.AddHttpClient();

        builder
            .SetCorsPolicy()
            .RegisterDocumentation();

        builder.Services.Configure<JsonOptions>(options =>
        {
            JsonSerialization.SetOptions(options.JsonSerializerOptions);
        });

        var domain = $"https://{builder.Configuration[$"{authConfigSectionName}:Domain"]}/";
        var audience = builder.Configuration[$"{authConfigSectionName}:Audience"];
        var scope = builder.Configuration[$"{authConfigSectionName}:Scope"];

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
            {
                c.Authority = domain;
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = audience,
                    ValidIssuer = domain
                };
            });

        builder.Services.AddAuthorization(options => options.AddPolicy(scope, policy => policy.Requirements.Add(new
            HasScopeRequirement(scope, domain))));

        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        return builder;
    }

    public static WebApplication CreateApp(WebApplicationBuilder builder)
    {
        var scope = builder.Configuration["ApiAuthentication:Scope"];

        var app = builder.Build();

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors();
        app.MapControllers().RequireAuthorization(scope);
        app.AddDocumentation();
        app.AddDocumentationUI();

        return app;
    }
}