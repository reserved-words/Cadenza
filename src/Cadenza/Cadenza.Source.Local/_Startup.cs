global using Cadenza.Common;
global using System.Net.Http.Json;
global using Cadenza.Domain;
global using Cadenza.Utilities;
global using Cadenza.Library;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cadenza.Core;
using Microsoft.JSInterop;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Local;

public static class Startup
{
    public static IServiceCollection AddLocalSource<TAudioPlayer>(this IServiceCollection services) where TAudioPlayer : class, IAudioPlayer
    {
        return services
            .AddDynamicSourceLibrary<LocalApi>(LibrarySource.Local)
            .AddTransient<LocalLibraryUpdater>()
            .AddTransient<ISourcePlayer>(sp => sp.GetLocalPlayer())
            .AddTransient<IFileUpdateQueue, LocalLibraryUpdater>()
            .AddTransient<ISearchRepository, LocalSearchRepository>();
    }

    public static ISourcePlayer GetLocalPlayer(this IServiceProvider services)
    {
        var jsRuntime = services.GetRequiredService<IJSRuntime>();
        var htmlPlayer = new HtmlPlayer(jsRuntime);
        var settings = services.GetRequiredService<IOptions<LocalApiSettings>>();
        return new LocalPlayer(htmlPlayer, settings);
    }

    public static IServiceCollection ConfigureLocalApi(this IServiceCollection services, IConfiguration config, params string[] sections)
    {
        return services.ConfigureOptions<LocalApiSettings>(config, sections);
    }
}
