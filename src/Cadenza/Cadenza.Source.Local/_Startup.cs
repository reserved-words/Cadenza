global using Cadenza.Domain;
global using Cadenza.Library;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Cadenza.Core.Interfaces;
using Cadenza.Core;

namespace Cadenza.Source.Local;

public static class Startup
{
    public static IServiceCollection AddLocalSource<TAudioPlayer>(this IServiceCollection services, string apiSectionName) where TAudioPlayer : class, IAudioPlayer
    {
        services.AddOptions<LocalApiSettings>(apiSectionName);

        return services
            .AddTransient<ISourcePlayer>(sp => new LocalPlayer(
                sp.GetRequiredService<TAudioPlayer>(),
                sp.GetRequiredService<IOptions<LocalApiSettings>>(),
                sp.GetRequiredService<IUrl>()))
            .AddApiRepositories<LocalApiRepositorySettings>(LibrarySource.Local)
            .AddTransient<IConnectionTaskBuilder, LocalConnectionTaskBuilder>();
    }
}
