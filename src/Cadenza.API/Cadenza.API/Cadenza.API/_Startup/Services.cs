namespace Cadenza.API;

public static class Services
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
