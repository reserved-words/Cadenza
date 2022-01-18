global using Cadenza.Common;
global using System.Net.Http.Json;
global using System.Web;
global using Cadenza.Domain;
global using Cadenza.Utilities;
global using Cadenza.Library;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Source.Local;

public static class Startup
{
    public static IServiceCollection AddLocalSource<TAudioPlayer>(this IServiceCollection services) where TAudioPlayer : class, IAudioPlayer
    {
        return services
            .AddTransient<IAudioPlayer, TAudioPlayer>()
            .AddDynamicSourceLibrary<LocalApi>(LibrarySource.Local)
            .AddTransient<LocalLibraryUpdater>()
            .AddTransient<LocalPlayer>()
            .AddTransient<IFileUpdateQueue, LocalLibraryUpdater>();
    }

    public static IAudioPlayer GetLocalPlayer(this IServiceProvider services)
    {
        return services.GetRequiredService<LocalPlayer>();
    }

    public static IServiceCollection ConfigureLocalApi(this IServiceCollection services, IConfiguration config, params string[] sections)
    {
        return services.ConfigureOptions<LocalApiSettings>(config, sections);
    }
}
