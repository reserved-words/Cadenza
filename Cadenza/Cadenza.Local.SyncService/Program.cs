
var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
    ?? "appsettings.json";

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile(settingsPath, false)
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<LoggerOptions>(configuration.GetSection("Logging"));
        services.Configure<LibraryPaths>(configuration.GetSection("LibraryPaths"));
        services.Configure<CurrentlyPlaying>(configuration.GetSection("CurrentlyPlaying"));
        services.Configure<MusicLibrary>(configuration.GetSection("MusicLibrary"));

        services
            .AddHostedService<Worker>()
            .AddSingleton<IConfiguration>(configuration)
            .AddTransient<IAddedFilesHandler, AddedFilesHandler>()
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
    })
    .UseWindowsService();

host.Build().Run();