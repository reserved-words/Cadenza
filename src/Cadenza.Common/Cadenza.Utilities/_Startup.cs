using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Utilities;

public static class _Startup
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        return services
            .AddTransient<IBase64Converter, Base64Converter>()
            .AddTransient<IDateTime, CurrentDateTime>()
            .AddTransient<IHasher, Hasher>()
            .AddTransient<IHttpHelper, HttpHelper>()
            .AddTransient<IIdGenerator, IdGenerator>()
            .AddTransient<IJsonConverter, JsonConverter>()
            .AddTransient<IListComparer, ListComparer>()
            .AddTransient<INameComparer, NameComparer>()
            .AddTransient<IRandomGenerator, RandomGenerator>()
            .AddTransient<IShuffler, Shuffler>();
    }


    public static IServiceCollection AddHttpClient(this IServiceCollection services, HttpClient client)
    {
        return services.AddTransient(sp => client)
            .AddTransient<IHttpHelper, HttpHelper>();
    }

    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        return services.AddTransient<HttpClient>()
            .AddTransient<IHttpHelper, HttpHelper>()
            .AddTransient<ILogger, Logger>();
    }

    public static IServiceCollection AddLogger(this IServiceCollection services, HttpClient client)
    {
        return services.AddTransient(sp => client)
            .AddTransient<IHttpHelper, HttpHelper>()
            .AddTransient<ILogger, Logger>();
    }

    public static IServiceCollection ConfigureLogger(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<LoggerOptions>(section);
    }
}
