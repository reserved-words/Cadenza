global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Apps.API;

public static class API
{
    public static WebApplicationBuilder CreateBuilder(string[] args, Action<IServiceCollection, IConfiguration> registerDependencies)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.RegisterConfiguration();

        registerDependencies(builder.Services, builder.Configuration);

        return builder
            .RegisterCorsPolicies()
            .RegisterDocumentation();
    }

    public static WebApplication CreateApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        app.AddCors();
        app.AddDocumentation();
        app.MapControllers();
        app.AddDocumentationUI();

        return app;
    }
}