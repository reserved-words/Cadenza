
var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
    ?? "appsettings.json";

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile(settingsPath, false)
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddTransient<ILibraryConfiguration, ServiceConfiguration>();
        services.AddTransient<IMusicDirectoryConfiguration, ServiceConfiguration>();
        services.AddTransient<ILoggerConfig, LoggerConfig>();

        services.AddTransient<IAddedFilesHandler, AddedFilesHandler>();
        services.AddTransient<IDeletedFilesHandler, DeletedFilesHandler>();
        services.AddTransient<IModifiedFilesHandler, ModifiedFilesHandler>();
        services.AddTransient<IPlayedFilesHandler, PlayedFilesHandler>();
        services.AddTransient<IUpdateQueueHandler, UpdateQueueHandler>();

        services.AddTransient<IArtistConverter, ArtistConverter>();
        services.AddTransient<IAlbumConverter, AlbumConverter>();
        services.AddTransient<IAlbumTrackLinkConverter, AlbumTrackLinkConverter>();
        services.AddTransient<ITrackConverter, TrackConverter>();

        services.AddTransient<IBase64Converter, Base64Converter>();
        services.AddTransient<ICommentProcessor, CommentProcessor>();
        services.AddTransient<IDataAccess, DataAccess>();
        services.AddTransient<IDateTime, CurrentDateTime>();
        services.AddTransient<IFileAccess, Cadenza.Local.FileAccess>();
        services.AddTransient<IFileUpdateService, FileUpdateService>();
        services.AddTransient<IId3TagsService, Id3TagsService>();
        services.AddTransient<IId3ToJsonConverter, Id3ToJsonConverter>();
        services.AddTransient<IId3Updater, Id3Updater>();
        services.AddTransient<IIdGenerator, IdGenerator>();
        services.AddTransient<IJsonConverter, JsonConverter>();
        services.AddTransient<IJsonMerger, JsonMerger>();
        services.AddTransient<ILibraryOrganiser, LibraryOrganiser>();
        services.AddTransient<IListComparer, ListComparer>();
        services.AddTransient<ILogger, Logger>();
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<IMusicDirectory, MusicDirectoryAccess>();
        services.AddTransient<INameComparer, NameComparer>();
        services.AddTransient<ITimeSpanConverter, TimeSpanConverter>();
        services.AddTransient<IUpdatedFilesFetcher, UpdatedFilesFetcher>();
        services.AddTransient<IUpdateHistory, UpdateHistory>();
        services.AddTransient<IUpdater, Updater>();
        services.AddTransient<IValueMerger, ValueMerger>();
    })
    .UseWindowsService();

host.Build().Run();