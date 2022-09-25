using Cadenza.Local.API.Common.Controllers;
using Cadenza.Local.API.Core;
using Cadenza.Local.API.Files;
using Cadenza.Utilities.Implementations;
using Cadenza.Utilities.Interfaces;

namespace Cadenza.Local.API;

public static class Dependencies
{
    public static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.RegisterDependencies();

        return builder;
    }

    private static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services.AddTransient<IValueMerger, ValueMerger>();
        services.AddTransient<IMerger, Merger>();

        services
            .AddUtilities()
            .AddMusicFileArtwork()
            .AddFileAccess()
            .AddCoreServices();

        return services;
    }
}
