global using Cadenza.Common;
global using Cadenza.Library;
global using System.Net.Http.Json;
global using System.Web;

using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Source.Local;

public static class Startup
{
    public static IServiceCollection AddLocalSource<TAudioPlayer>(this IServiceCollection services) where TAudioPlayer : class, IAudioPlayer
    {
        return services
            .AddTransient<IAudioPlayer, TAudioPlayer>()
            .AddTransient<LocalLibraryUpdater>()
            .AddTransient<LocalLibrary>()
            .AddTransient<LocalPlayer>()
            .AddTransient<IFileUpdateQueue, LocalLibraryUpdater>();
    }

    public static IAudioPlayer GetLocalPlayer(this IServiceProvider services)
    {
        return services.GetRequiredService<LocalPlayer>();
    }

    public static ISourceRepository GetLocalRepository(this IServiceProvider services)
    {
        return services.GetRequiredService<LocalLibrary>();
    }

    public static ISourceLibraryUpdater GetLocalUpdater(this IServiceProvider services)
    {
        return services.GetRequiredService<LocalLibraryUpdater>();
    }
}
