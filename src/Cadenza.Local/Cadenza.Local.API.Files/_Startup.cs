using Cadenza.Local.API.Common.Interfaces;
using Cadenza.Local.API.Files.Interfaces;
using Cadenza.Local.API.Files.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Local.API.Files;
public static class _Startup
{
    public static IServiceCollection AddMusicFileLibrary(this IServiceCollection services)
    {
        return services
            .AddTransient<ICommentProcessor, CommentProcessor>()
            .AddTransient<IId3TagsService, Id3TagsService>()
            .AddTransient<IId3ToModelConverter, Id3ToModelConverter>()
            .AddTransient<IMusicFileLibrary, MusicFileLibrary>();
    }

    public static IServiceCollection AddMusicFileArtwork(this IServiceCollection services)
    {
        return services
            .AddTransient<IId3TagsService, Id3TagsService>()
            .AddTransient<IMusicFileArtworkService, MusicFileArtworkService>();
    }
}
