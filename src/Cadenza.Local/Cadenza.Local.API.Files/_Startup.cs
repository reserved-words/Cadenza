global using Cadenza.Domain.Enums;
global using Cadenza.Domain.Extensions;
global using Cadenza.Domain.Model.Album;
global using Cadenza.Domain.Model.Artist;
global using Cadenza.Domain.Model.Track;
global using Cadenza.Domain.Model.Updates;

global using Cadenza.Local.API.Files.Model;
global using Cadenza.Local.API.Common.Interfaces;
global using Cadenza.Local.API.Files.Interfaces;
global using Cadenza.Local.API.Files.Services;

global using Cadenza.Utilities.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Local.API.Files;
public static class _Startup
{
    public static IServiceCollection AddMusicService(this IServiceCollection services)
    {
        return services
            .AddTransient<ICommentProcessor, CommentProcessor>()
            .AddTransient<IId3Fetcher, Id3Fetcher>()
            .AddTransient<IId3TagsService, Id3TagsService>()
            .AddTransient<IId3ToModelConverter, Id3ToModelConverter>()
            .AddTransient<IId3Updater, Id3Updater>()
            .AddTransient<IMusicFilesService, MusicService>();
    }

    public static IServiceCollection AddArtworkService(this IServiceCollection services)
    {
        return services
            .AddTransient<IId3TagsService, Id3TagsService>()
            .AddTransient<IArtworkFilesService, ArtworkService>();
    }
}
