using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.MusicFiles;
using Cadenza.Local.Services;
using Cadenza.Utilities.Implementations;
using Cadenza.Utilities.Interfaces;
using FileAccess = Cadenza.Local.Services.FileAccess;

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
            .AddLogger()
            .AddMusicFileArtwork()
            .AddTransient<IFileAccess, FileAccess>()
            .AddTransient<IJsonConverter, JsonConverter>()
            .AddTransient<IImageSrcGenerator, ImageSrcGenerator>()
            .AddTransient<IArtworkService, ArtworkService>()
            .AddTransient<IPlayService, PlayService>();

        return services;
    }
}
