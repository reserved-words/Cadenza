using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Cadenza.Common.Utilities.Tests")]
[assembly: InternalsVisibleTo("TestConsoleApp")]

namespace Cadenza.Local.FileAccess;

public static class _Startup
{
    public static IServiceCollection AddFileAccess(this IServiceCollection services)
    {
        return services.AddTransient<IFileAccess, Service>();
    }
}
