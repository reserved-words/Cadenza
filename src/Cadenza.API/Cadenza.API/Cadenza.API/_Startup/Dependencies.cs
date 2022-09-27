namespace Cadenza.API._Startup;

public static class Dependencies
{
    public static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddCoreServices()
            .AddJsonLibrary()
            .AddLastFM()
            .AddUtilities()
            .AddScoped(sp => new HttpClient());

        return builder;
    }
}
