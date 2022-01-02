using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadenza.Local.SyncService
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAddedFilesHandler, AddedFilesHandler>()
               .AddTransient<IDeletedFilesHandler, DeletedFilesHandler>()
               .AddTransient<IModifiedFilesHandler, ModifiedFilesHandler>()
               .AddTransient<IPlayedFilesHandler, PlayedFilesHandler>()
               .AddTransient<IUpdateQueueHandler, UpdateQueueHandler>()
               .AddTransient<IArtistConverter, ArtistConverter>()
               .AddTransient<IAlbumConverter, AlbumConverter>()
               .AddTransient<IAlbumTrackLinkConverter, AlbumTrackLinkConverter>()
               .AddTransient<ITrackConverter, TrackConverter>()
               .AddTransient<IBase64Converter, Base64Converter>()
               .AddTransient<ICommentProcessor, CommentProcessor>()
               .AddTransient<IDataAccess, DataAccess>()
               .AddTransient<IDateTime, CurrentDateTime>()
               .AddTransient<IFileAccess, Cadenza.Local.FileAccess>()
               .AddTransient<IFileUpdateService, FileUpdateService>()
               .AddTransient<IId3TagsService, Id3TagsService>()
               .AddTransient<IId3ToJsonConverter, Id3ToJsonConverter>()
               .AddTransient<IId3Updater, Id3Updater>()
               .AddTransient<IIdGenerator, IdGenerator>()
               .AddTransient<IJsonConverter, JsonConverter>()
               .AddTransient<IJsonMerger, JsonMerger>()
               .AddTransient<ILibraryOrganiser, LibraryOrganiser>()
               .AddTransient<IListComparer, ListComparer>()
               .AddTransient<ILogger, Logger>()
               .AddTransient<IMerger, Merger>()
               .AddTransient<IMusicDirectory, MusicDirectoryAccess>()
               .AddTransient<INameComparer, NameComparer>()
               .AddTransient<IUpdatedFilesFetcher, UpdatedFilesFetcher>()
               .AddTransient<IUpdateHistory, UpdateHistory>()
               .AddTransient<IUpdater, Updater>()
               .AddTransient<IValueMerger, ValueMerger>();

            return services;
        }
    }
}
