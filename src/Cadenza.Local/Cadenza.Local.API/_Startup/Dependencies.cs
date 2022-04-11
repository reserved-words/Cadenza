using Cadenza.Library;
using Cadenza.Local.API.Interfaces;
using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.Cache;
using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.MusicFiles;
using Cadenza.Local.Services;
using Cadenza.Local.Services.Cache;
using Cadenza.Local.Services.Converters;
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
        services.AddSingleton<IArtistCache, ArtistCache>();
        services.AddSingleton<IAlbumCache, AlbumCache>();
        services.AddSingleton<IPlayTrackCache, PlayTrackCache>();
        services.AddSingleton<ISearchCache, SearchCache>();
        services.AddSingleton<ITrackCache, TrackCache>();
        services.AddTransient<IValueMerger, ValueMerger>();
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<ILibrary, JsonLibrary>();

        services
            .AddUtilities()
            .AddLogger()
            .AddMusicFileLibrary()
            .AddTransient<IDataAccess, DataAccess>()
            .AddTransient<IFileAccess, FileAccess>()
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
            .AddTransient<ILibraryService, LibraryService>()
            .AddTransient<IUpdateService, UpdateService>()
            .AddTransient<IExternalSourceService, ExternalSourceService>();

        return services;
    }
}
