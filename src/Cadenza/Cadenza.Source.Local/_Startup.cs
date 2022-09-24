global using Cadenza.Domain;
global using Cadenza.Library;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Cadenza.Core.Interfaces;
using Cadenza.Core;
using Microsoft.Extensions.Configuration;
using Cadenza.Source.Local.Services;
using Cadenza.Domain.Enums;

namespace Cadenza.Source.Local;

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
            .AddApiRepositories<LocalApiRepositorySettings>(LibrarySource.Local)
            .AddTransient<IConnectionTaskBuilder, LocalConnectionTaskBuilder>()
            .AddTransient<IFileUpdateQueue, UpdateQueueService>();
    }
}
