global using Cadenza.Web.Common.Interfaces;
global using Microsoft.Extensions.DependencyInjection;
using Cadenza.Web.Info.Interfaces;
using Cadenza.Web.Info.Services;

namespace Cadenza.Web.Info;

public static class _Startup
{
    public static IServiceCollection AddWebInfo(this IServiceCollection services)
    {
        return services
            .AddTransient<IWebInfoService, WebInfoService>()
            .AddTransient<IWebInfoHttpHelper, WebInfoHttpHelper>();
    }
}