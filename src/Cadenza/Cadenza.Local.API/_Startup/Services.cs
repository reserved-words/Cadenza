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
        var cache = new Cache();

        services.AddSingleton<ICache>(sp => cache)
            .AddUtilities()
            .AddLogger()
            .AddTransient<JsonLibrary>()
            .AddTransient<List<IStaticSource>>(sp => new List<IStaticSource> { sp.GetService<JsonLibrary>() })
            .AddTransient<ILibrary, CombinedStaticLibrary>()

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
            .AddTransient<LibraryUpdater>()
            .AddTransient<Id3UpdateQueuer>()
            .AddTransient<ICollection<ILibraryUpdater>>(sp => new List<ILibraryUpdater>
            {
                sp.GetService<JsonUpdater>(),
                sp.GetService<LibraryUpdater>(),
                sp.GetService<Id3UpdateQueuer>()
            })
            .AddTransient<IStaticSource, JsonLibrary>()
            .AddTransient<ILibraryService, LibraryService>()
            .AddTransient<IPlayService, PlayService>()
            .AddTransient<IUpdateService, UpdateService>();

        return services;
    }
}
