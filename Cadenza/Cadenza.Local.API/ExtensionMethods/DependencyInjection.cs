namespace Cadenza.Local.API;

public static class DependencyInjection
{
    public static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.RegisterDependencies();
        return builder;
    }

    private static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        var cache = new Cache();

        services.AddSingleton<ICache>(sp => cache);
        services.AddTransient<JsonLibrary>();
        services.AddTransient<List<IStaticSource>>(sp => new List<IStaticSource> { sp.GetService<JsonLibrary>() });
        services.AddTransient<ILibrary, CombinedStaticLibrary>();

        services.AddTransient<IBase64Converter, Base64Converter>();
        services.AddTransient<ICommentProcessor, CommentProcessor>();
        services.AddTransient<IDataAccess, DataAccess>();
        services.AddTransient<IFileAccess, FileAccess>();
        services.AddTransient<IId3TagsService, Id3TagsService>();
        services.AddTransient<IJsonConverter, JsonConverter>();
        services.AddTransient<IJsonMerger, JsonMerger>();
        services.AddTransient<ILibraryConfiguration, AppConfiguration>();
        services.AddTransient<INameComparer, NameComparer>();
        services.AddTransient<ITimeSpanConverter, TimeSpanConverter>();
        services.AddTransient<IFileUpdateService, FileUpdateService>();
        services.AddTransient<IIdGenerator, IdGenerator>();
        services.AddTransient<IImageSrcGenerator, ImageSrcGenerator>();
        services.AddTransient<IArtistConverter, ArtistConverter>();
        services.AddTransient<IAlbumConverter, AlbumConverter>();
        services.AddTransient<ITrackConverter, TrackConverter>();
        services.AddTransient<IAlbumTrackLinkConverter, AlbumTrackLinkConverter>();
        services.AddTransient<IJsonToModelConverter, JsonToModelConverter>();
        services.AddTransient<IModelToJsonConverter, ModelToJsonConverter>();
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<IValueMerger, ValueMerger>();

        services.AddTransient<JsonUpdater>();
        services.AddTransient<LibraryUpdater>();
        services.AddTransient<Id3UpdateQueuer>();
        services.AddTransient<ICollection<ILibraryUpdater>>(sp => new List<ILibraryUpdater>
            {
                sp.GetService<JsonUpdater>(),
                sp.GetService<LibraryUpdater>(),
                sp.GetService<Id3UpdateQueuer>()
            });

        services.AddTransient<IStaticSource, JsonLibrary>();
        services.AddTransient<ILibraryService, LibraryService>();
        services.AddTransient<IPlayService, PlayService>();
        services.AddTransient<IUpdateService, UpdateService>();

        return services;
    }
}
