namespace Cadenza.API._Startup;

public static class Dependencies
{
    public static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAzure()
            .AddLastFM()
            .AddSpotify()
            .AddUtilities()
            .AddLogger();

        return builder;
    }
}
