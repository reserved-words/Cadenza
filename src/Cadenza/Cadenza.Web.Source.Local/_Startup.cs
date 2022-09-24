using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Source.Local.Settings;
using Cadenza.Web.Source.Local.Services;

namespace Cadenza.Web.Source.Local;

public static class Startup
{
    public static IServiceCollection AddLocalSource<TAudioPlayer>(this IServiceCollection services, IConfiguration config, string apiSectionName) where TAudioPlayer : class, IAudioPlayer
    {
        services.Configure<LocalApiSettings>(config.GetSection(apiSectionName));

        return services
            .AddTransient<TAudioPlayer>()
            .AddTransient<ISourcePlayer>(sp => new LocalPlayer(
                sp.GetRequiredService<TAudioPlayer>(),
                sp.GetRequiredService<IOptions<LocalApiSettings>>(),
                sp.GetRequiredService<IUrl>()))
            .AddTransient<IConnectionTaskBuilder, LocalConnectionTaskBuilder>();
    }
}
