using Cadenza.Library;
using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Interfaces.FileProcessors;
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
           .AddTransient<IAddedFilesHandler, AddedFilesHandler>()
           .AddTransient<IDeletedFilesHandler, DeletedFilesHandler>()
           .AddTransient<IModifiedFilesHandler, ModifiedFilesHandler>()
           .AddTransient<IPlayedFilesHandler, PlayedFilesHandler>()
           .AddTransient<IUpdateQueueHandler, UpdateQueueHandler>()
           .AddTransient<IArtistConverter, ArtistConverter>()
           .AddTransient<IAlbumConverter, AlbumConverter>()
           .AddTransient<IAlbumTrackLinkConverter, AlbumTrackLinkConverter>()
           .AddTransient<ITrackConverter, TrackConverter>()
           .AddTransient<ICommentProcessor, CommentProcessor>()
           .AddTransient<IDataAccess, DataAccess>()
           .AddTransient<IFileAccess, FileAccess>()
           .AddTransient<IFileUpdateService, FileUpdateService>()
           .AddTransient<IId3TagsService, Id3TagsService>()
           .AddTransient<IId3ToJsonConverter, Id3ToJsonConverter>()
           .AddTransient<IId3Updater, Id3Updater>()
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