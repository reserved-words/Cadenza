using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Wrapper.Core;

public static class _Startup
{
     public static IServiceCollection ConfigureCoreAPI(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<ApiSettings>(section);
    }

    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IUrl, Url>()
            .AddTransient<IConnectionChecker, ConnectionChecker>();
    }
}