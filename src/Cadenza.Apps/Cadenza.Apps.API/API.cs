global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
using Cadenza.Apps.API.Extensions;

namespace Cadenza.Apps.API;

public static class API
{
    public static WebApplicationBuilder CreateBuilder(string authConfigSectionName, Action<IServiceCollection, IConfiguration> registerDependencies)
    {
        var builder = WebApplication.CreateBuilder(Array.Empty<string>());

        return builder
            .ConfigureLogging()
            .RegisterDependencies(registerDependencies)
            .SetCorsPolicy()
            .RegisterDocumentation()
            .ConfigureJsonConverter()
            .ConfigureAuthentication(authConfigSectionName);
    }

    public static WebApplication CreateApp(WebApplicationBuilder builder, string authConfigSectionName)
    {
        var app = builder.Build();

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors();
        app.MapControllers().RequireAuthorization(builder.Configuration, authConfigSectionName);
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