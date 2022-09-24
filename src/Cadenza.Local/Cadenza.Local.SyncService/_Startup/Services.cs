﻿using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.MusicFiles;
using Cadenza.Local.Services;
using Cadenza.Local.SyncService.Updaters;
using Cadenza.Utilities.Implementations;
using Cadenza.Utilities.Interfaces;
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
           .AddTransient<IFileAccess, FileAccess>()
           //.AddTransient<ILocalFilesUpdater, LocalFileUpdater>()
           .AddTransient<IJsonConverter, JsonConverter>()
           .AddTransient<IMusicDirectory, MusicDirectoryAccess>()
           .AddTransient<IUpdatedFilesFetcher, UpdatedFilesFetcher>()
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