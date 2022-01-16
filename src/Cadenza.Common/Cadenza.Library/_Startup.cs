global using Cadenza.Domain;
global using Cadenza.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Library;

public static class _Startup
{
    public static IServiceCollection AddLibrary(this IServiceCollection services)
    {
        return services
            .AddTransient<IMerger, Merger>()
            .AddTransient<IValueMerger, ValueMerger>();

    }
}