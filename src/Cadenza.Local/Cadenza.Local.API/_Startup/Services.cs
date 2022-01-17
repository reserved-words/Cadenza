using Cadenza.Library;

namespace Cadenza.Local.API;

public static class Services
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
            .AddCombinedLibrary<JsonLibrary>()
            .AddTransient<ILibraryUpdater, JsonUpdater>()
            .AddTransient<ILibraryUpdater, Id3UpdateQueuer>()
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
            .AddTransient<JsonUpdater>()
            .AddTransient<Id3UpdateQueuer>()
            .AddTransient<ILibraryService, LibraryService>()
            .AddTransient<IPlayService, PlayService>()
            .AddTransient<IUpdateService, UpdateService>();

        return services;
    }
}
