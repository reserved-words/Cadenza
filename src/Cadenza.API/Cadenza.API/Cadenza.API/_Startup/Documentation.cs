namespace Cadenza.API._Startup;

public static class Documentation
{
    public static WebApplicationBuilder RegisterDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        return builder;
    }

    public static WebApplication AddDocumentation(this WebApplication app)
    {
        app.UseSwagger();
        return app;
    }

    public static WebApplication AddDocumentationUI(this WebApplication app)
    {
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
        return app;
    }
}