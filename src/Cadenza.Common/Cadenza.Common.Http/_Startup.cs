global using Cadenza.Common.Model;
global using System.Net.Http.Json;

using Cadenza.Common.Http.Interfaces;
using Cadenza.Common.Http.Services;
using Microsoft.Extensions.DependencyInjection;

public static class _Startup
{
    public static IServiceCollection AddHttpHelper(this IServiceCollection services)
    {
        return services
            .AddTransient<IHttpRequestSender, HttpRequestSender>()
            .AddTransient<IHttpHelper, DefaultHttpHelper>();
    }
}