global using Cadenza.Common.Model;
global using System.Net.Http.Json;
using Cadenza.Common.Http;
using Microsoft.Extensions.DependencyInjection;

public static class _Startup
{
    public static IServiceCollection AddHttpHelper(this IServiceCollection services)
    {
        return services.AddTransient<IHttpRequestSender, HttpRequestSender>();
    }

    public static IServiceCollection AddDefaultHttpHelper(this IServiceCollection services)
    {
        return services.AddTransient<IHttpHelper, DefaultHttpHelper>();
    }
}