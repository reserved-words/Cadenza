global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Model.Album;
global using Cadenza.Common.Interfaces.Repositories;
global using Cadenza.Common.Interfaces.Utilities;
global using Cadenza.Common.Utilities.Extensions;
global using Cadenza.Web.Common.Enums;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.Common.Tasks;
global using Cadenza.Web.Source.Local.Services;
global using Cadenza.Web.Source.Local.Settings;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;

namespace Cadenza.Web.Source.Local;

public static class Startup
{
    public static IServiceCollection AddLocalSource<TAudioPlayer>(this IServiceCollection services) where TAudioPlayer : class, IAudioPlayer
    {
        return services
            .AddTransient<TAudioPlayer>()
            .AddTransient<ISourcePlayer>(sp => new LocalPlayer(
                sp.GetRequiredService<TAudioPlayer>(),
                sp.GetRequiredService<IOptions<LocalApiSettings>>(),
                sp.GetRequiredService<IUrl>()))
            .AddTransient<IConnector, LocalSourceConnector>()
            .AddTransient<ISourceArtworkFetcher, LocalArtworkFetcher>();
    }
}
