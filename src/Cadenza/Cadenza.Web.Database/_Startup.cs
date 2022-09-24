global using Cadenza.Domain;
global using Cadenza.Library;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Cadenza.Domain.Enums;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Database.Settings;
using Cadenza.Web.Database.Services;

namespace Cadenza.Web.Database;

public static class Startup
{
    public static IServiceCollection AddLocalSource<TAudioPlayer>(this IServiceCollection services, IConfiguration config, string apiSectionName) where TAudioPlayer : class, IAudioPlayer
    {
        services.Configure<DatabaseApiSettings>(config.GetSection(apiSectionName));

        return services
            .AddTransient<TAudioPlayer>()
            .AddTransient<ISourcePlayer>(sp => new LocalPlayer(
                sp.GetRequiredService<TAudioPlayer>(),
                sp.GetRequiredService<IOptions<DatabaseApiSettings>>(),
                sp.GetRequiredService<IUrl>()))
            .AddApiRepositories<DatabaseApiRepositorySettings>(LibrarySource.Local)
            .AddTransient<IConnectionTaskBuilder, LocalConnectionTaskBuilder>()
            .AddTransient<IFileUpdateQueue, UpdateQueueService>();
    }
}
