global using Cadenza.Common.Domain.Model;
global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Extensions;
global using Cadenza.Common.Domain.Model.Album;
global using Cadenza.Common.Domain.Model.Artist;
global using Cadenza.Common.Domain.Model.Track;
global using Cadenza.Common.Domain.Model.Updates;
global using Cadenza.Common.Interfaces.Utilities;
global using Cadenza.Local.API.Common.Interfaces;
global using Cadenza.Local.API.Files.Interfaces;
global using Cadenza.Local.API.Files.Model;
global using Cadenza.Local.API.Files.Services;

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
}
