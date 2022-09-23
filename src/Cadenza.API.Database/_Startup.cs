using Cadenza.API.Common.Interfaces;
using Cadenza.API.Database.Interfaces;
using Cadenza.API.Database.Services;
using Microsoft.Extensions.DependencyInjection;
using FileAccess = Cadenza.API.Database.Services.FileAccess;

namespace Cadenza.API.Database;

public static class _Startup
{
    public static IServiceCollection AddJsonLibrary(this IServiceCollection services)
    {
        return services
            .AddInternalServices()
            .AddTransient<ILibrary, Library>();
    }

    private static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IAlbumConverter, AlbumConverter>()
            .AddTransient<IAlbumTrackLinkConverter, AlbumTrackLinkConverter>()
            .AddTransient<IArtistConverter, ArtistConverter>()
            .AddTransient<IDataAccess, DataAccess>()
            .AddTransient<IFileAccess, FileAccess>()
            .AddTransient<IJsonMerger, JsonMerger>()
            .AddTransient<IJsonToModelConverter, JsonToModelConverter>()
            .AddTransient<ILibraryOrganiser, LibraryOrganiser>()
            .AddTransient<ITrackConverter, TrackConverter>();
    }
}
