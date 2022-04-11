using Cadenza.Library;
using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Interfaces.FileProcessors;
using Cadenza.Local.MusicFiles;
using Cadenza.Local.Services;
using Cadenza.Local.Services.Converters;
using Cadenza.Local.Services.FileProcessors;
using FileAccess = Cadenza.Local.Services.FileAccess;

namespace Cadenza.Local.SyncService;

public static class Services
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services
           .AddUtilities()
           .AddLogger()
           .AddMusicFileLibrary()
           .AddTransient<IAddedFilesHandler, AddedFilesHandler>()
           .AddTransient<IDeletedFilesHandler, DeletedFilesHandler>()
           .AddTransient<IModifiedFilesHandler, ModifiedFilesHandler>()
           .AddTransient<IPlayedFilesHandler, PlayedFilesHandler>()
           .AddTransient<IUpdateQueueHandler, UpdateQueueHandler>()
           .AddTransient<IArtistConverter, ArtistConverter>()
           .AddTransient<IAlbumConverter, AlbumConverter>()
           .AddTransient<IAlbumTrackLinkConverter, AlbumTrackLinkConverter>()
           .AddTransient<ITrackConverter, TrackConverter>()
           .AddTransient<IDataAccess, DataAccess>()
           .AddTransient<IFileAccess, FileAccess>()
           .AddTransient<IFileUpdateService, FileUpdateService>()
           .AddTransient<IMusicFilesUpdater, MusicFilesUpdater>()
           .AddTransient<IJsonConverter, JsonConverter>()
           .AddTransient<IJsonMerger, JsonMerger>()
           .AddTransient<ILibraryOrganiser, LibraryOrganiser>()
           .AddTransient<IMusicDirectory, MusicDirectoryAccess>()
           .AddTransient<IUpdatedFilesFetcher, UpdatedFilesFetcher>()
           .AddTransient<IUpdateHistory, UpdateHistory>()
           .AddTransient<ILocalLibraryUpdater, LocalLibraryUpdater>()
           .AddTransient<IMerger, Merger>();

        return services;
    }
}