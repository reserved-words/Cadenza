using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Source.Spotify.Api;
public static class _Startup
{
    public static IServiceCollection AddSpotifyApi<TTokenProvider>(this IServiceCollection services) where TTokenProvider : class, ITokenProvider
    {
        return services
            .AddTransient<ITokenProvider, TTokenProvider>()
            .AddTransient<IApiHelper, ApiHelper>()
            .AddTransient<IDevicesApi, DevicesApi>()
            .AddTransient<IPlayerApi, PlayerApi>()
            .AddTransient<ISearchApi, SearchApi>()
            .AddTransient<IUserApi, UserApi>();
    }
}
