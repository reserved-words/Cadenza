global using Cadenza.Common.DTO;
global using Cadenza.Common.Enums;
global using Cadenza.Common.Model;
global using Cadenza.Common.Utilities.Interfaces;
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
            .AddTransient<IId3Updater, Id3Updater>()
            .AddTransient<IMusicFilesService, MusicService>();
    }
}
