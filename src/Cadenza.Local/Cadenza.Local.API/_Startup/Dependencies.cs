using Cadenza.Library;
using Cadenza.Local.API.Interfaces;

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
        services
            .AddUtilities()
            .AddLogger()
            .AddLibrary<JsonLibrary>()
            .AddBaseRepositories()
            .AddTransient<ICommentProcessor, CommentProcessor>()
            .AddTransient<IDataAccess, DataAccess>()
            .AddTransient<IFileAccess, FileAccess>()
            .AddTransient<IId3TagsService, Id3TagsService>()
            .AddTransient<IJsonConverter, JsonConverter>()
            .AddTransient<IJsonMerger, JsonMerger>()
            .AddTransient<IFileUpdateService, FileUpdateService>()
            .AddTransient<IImageSrcGenerator, ImageSrcGenerator>()
            .AddTransient<IArtistConverter, ArtistConverter>()
            .AddTransient<IAlbumConverter, AlbumConverter>()
            .AddTransient<ITrackConverter, TrackConverter>()
            .AddTransient<IAlbumTrackLinkConverter, AlbumTrackLinkConverter>()
            .AddTransient<IJsonToModelConverter, JsonToModelConverter>()
            .AddTransient<IArtworkService, ArtworkService>()
            .AddTransient<IPlayService, PlayService>()
            .AddTransient<IApiLibraryService, ApiLibraryService>()
            .AddTransient<IApiUpdateService, ApiUpdateService>();

        return services;
    }
}
