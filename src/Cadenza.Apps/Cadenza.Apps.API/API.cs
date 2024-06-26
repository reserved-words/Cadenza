﻿global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
using Cadenza.Apps.API.Extensions;
using System.Text.Json;

namespace Cadenza.Apps.API;

public static class API
{
    public static WebApplicationBuilder CreateBuilder(
        Action<IServiceCollection, IConfiguration> registerDependencies,
        Action<JsonSerializerOptions> setJsonOptions)
    {
        var builder = WebApplication.CreateBuilder(Array.Empty<string>());

        return builder
            .ConfigureLogging()
            .RegisterDependencies(registerDependencies)
            .SetCorsPolicy()
            .RegisterDocumentation()
            .ConfigureJsonSerialization(setJsonOptions);
    }

    public static WebApplication CreateApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        app.UseMiddleware<ErrorHandlingMiddleware>();
        //app.UseAuthentication();
        //app.UseAuthorization();
        app.UseCors();
        app.MapControllers();
        app.AddDocumentation();
        app.AddDocumentationUI();

        return app;
    }

    private static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder, Action<IServiceCollection, IConfiguration> registerDependencies)
    {
        registerDependencies(builder.Services, builder.Configuration);
        builder.Services.AddHttpClient();
        return builder;
    }
}