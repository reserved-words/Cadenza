global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
using Cadenza.Common.Domain.JsonConverters;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cadenza.Apps.API;

public static class API
{
    public static WebApplicationBuilder CreateBuilder(string[] args, Action<IServiceCollection, IConfiguration> registerDependencies)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.RegisterConfiguration();

        builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(ctx.Configuration.LogFilePath(), rollingInterval: RollingInterval.Day));

        registerDependencies(builder.Services, builder.Configuration);

        builder
            .RegisterCorsPolicies()
            .RegisterDocumentation();

        builder.Services.Configure<JsonOptions>(options =>
        {
            JsonSerialization.SetOptions(options.JsonSerializerOptions);
        });

        return builder;
    }

    public static WebApplication CreateApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.AddCors();
        app.AddDocumentation();
        app.MapControllers();
        app.AddDocumentationUI();

        return app;
    }
}