global using Cadenza.Common.Enums;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.Source.Local.Services;
global using Cadenza.Web.Source.Local.Settings;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Cadenza.Common.Http;
global using Cadenza.Web.Source.Local.Interfaces;

using Cadenza.Common.Utilities.Interfaces;

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
                sp.GetRequiredService<IUrl>(),
                sp.GetRequiredService<IBase64Encoder>()))
            .AddTransient<ILocalHttpHelper, LocalHttpHelper>();
    }
}
