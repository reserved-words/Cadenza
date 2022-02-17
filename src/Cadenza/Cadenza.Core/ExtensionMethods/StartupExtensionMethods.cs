using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Common;

public static class StartupExtensionMethods
{
    public static IServiceCollection ConfigureOptions<T>(this IServiceCollection services, IConfiguration config, params string[] sectionPath) where T : class
    {
        var section = config.GetSection(sectionPath[0]);

        for (var i = 1; i < sectionPath.Count(); i++)
        {
            section = section.GetSection(sectionPath[i]);
        }

        services.Configure<T>(section);
        return services;
    }
}