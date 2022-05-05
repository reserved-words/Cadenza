using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Source.Spotify.Api;
public static class _Startup
{
    public static IServiceCollection AddSpotifyApi<TTokenProvider, TAuthHelper>(this IServiceCollection services) 
        where TTokenProvider : class, ITokenProvider
        where TAuthHelper : class, IAuthHelper
    {
        return services
            .AddTransient<ITokenProvider, TTokenProvider>()
            .AddTransient<IAuthHelper, TAuthHelper>()
            .AddTransient<IApiHelper, ApiHelper>()
            .AddTransient<IAuthApi, AuthApi>()
            .AddTransient<IDevicesApi, DevicesApi>()
            .AddTransient<IPlayerApi, PlayerApi>()
            .AddTransient<ISearchApi, SearchApi>()
            .AddTransient<IUserApi, UserApi>();
    }
}
