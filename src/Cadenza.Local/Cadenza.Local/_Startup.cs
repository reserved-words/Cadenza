using Cadenza.Local.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FileAccess = Cadenza.Local.Services.FileAccess;

namespace Cadenza.Local
{
    public static class Startup
    {
        public static IServiceCollection AddFileAccess(this IServiceCollection services)
        {
            return services.AddTransient<IFileAccess, FileAccess>();
        }
    }
}
