using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.MusicFiles;
using Cadenza.Local.Services;
using Cadenza.Local.Services.Converters;
using Cadenza.Local.SyncService.Updaters;
using FileAccess = Cadenza.Local.Services.FileAccess;

namespace Cadenza.Local.SyncService._Startup;

public static class Services
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services
           .AddUtilities()
           .AddLogger()
           .AddMusicFileLibrary()
           .AddUpdaters()
           .AddTransient<IArtistConverter, ArtistConverter>()
           .AddTransient<IAlbumConverter, AlbumConverter>()
           .AddTransient<IAlbumTrackLinkConverter, AlbumTrackLinkConverter>()
           .AddTransient<ITrackConverter, TrackConverter>()
           .AddTransient<IDataAccess, DataAccess>()
           .AddTransient<IFileAccess, FileAccess>()
           .AddTransient<IFileUpdateService, FileUpdateService>()
           .AddTransient<ILocalFilesUpdater, LocalFileUpdater>()
           .AddTransient<IJsonConverter, JsonConverter>()
           .AddTransient<IJsonMerger, JsonMerger>()
           .AddTransient<ILibraryOrganiser, LibraryOrganiser>()
           .AddTransient<IMusicDirectory, MusicDirectoryAccess>()
           .AddTransient<IUpdatedFilesFetcher, UpdatedFilesFetcher>()
           .AddTransient<IUpdateHistory, UpdateHistory>()
           .AddTransient<IMerger, Merger>();

        return services;
    }

    private static IServiceCollection AddUpdaters(this IServiceCollection services)
    {
        return services
            .AddTransient<IUpdateService, AddedFilesHandler>()
            .AddTransient<IUpdateService, DeletedFilesHandler>()
            .AddTransient<IUpdateService, ModifiedFilesHandler>()
            .AddTransient<IUpdateService, PlayedFilesHandler>()
            .AddTransient<IUpdateService, UpdateQueueHandler>();
    }
}