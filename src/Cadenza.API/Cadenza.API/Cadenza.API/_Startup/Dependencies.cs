namespace Cadenza.API._Startup;

public static class Dependencies
{
    public static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddLastFM()
            .AddUtilities()
            .AddLogger();

        return builder;
    }
}
