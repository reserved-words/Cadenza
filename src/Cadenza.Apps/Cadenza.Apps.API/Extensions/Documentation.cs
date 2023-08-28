namespace Cadenza.Apps.API.Extensions;

internal static class Documentation
{
    internal static WebApplicationBuilder RegisterDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        return builder;
    }

    internal static WebApplication AddDocumentation(this WebApplication app)
    {
        app.UseSwagger();
        return app;
    }

    internal static WebApplication AddDocumentationUI(this WebApplication app)
    {
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
        return app;
    }
}
