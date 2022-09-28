namespace Cadenza.Local.API._Startup;

public static class Dependencies
{
    public static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.RegisterDependencies();

        return builder;
    }

    private static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services.AddScoped((sp) => new HttpClient());

        services
            .AddUtilities()
            .AddArtworkService()
            .AddMusicService()
            .AddCoreServices();

        return services;
    }
}
