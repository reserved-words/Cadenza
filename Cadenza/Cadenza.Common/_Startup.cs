using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Common;

public static class Startup
{
    public static IServiceCollection AddCommonUtilities(this IServiceCollection services)
    {
        return services
            .AddTransient<IBase64Converter, Base64Converter>()
            .AddTransient<IDateTime, CurrentDateTime>()
            .AddTransient<IHasher, Hasher>()
            .AddTransient<IHttpClient, HttpClient>()
            .AddTransient<IIdGenerator, IdGenerator>()
            .AddTransient<IListComparer, ListComparer>()
            .AddTransient<ILongRunningTaskService, LongRunningTaskService>()
            .AddTransient<IMerger, Merger>()
            .AddTransient<INameComparer, NameComparer>()
            .AddTransient<IRandomGenerator, RandomGenerator>()
            .AddTransient<IShuffler, Shuffler>()
            .AddTransient<IValueMerger, ValueMerger>();
    }

    public static IServiceCollection AddHttpClient(this IServiceCollection services, System.Net.Http.HttpClient client)
    {
        return services.AddTransient(sp => client)
            .AddTransient<IHttpClient, HttpClient>();
    }

    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        return services.AddTransient<System.Net.Http.HttpClient>()
            .AddTransient<IHttpClient, HttpClient>()
            .AddTransient<ILogger, Logger>();
    }

    public static IServiceCollection AddLogger(this IServiceCollection services, System.Net.Http.HttpClient client)
    {
        return services.AddTransient(sp => client)
            .AddTransient<IHttpClient, HttpClient>()
            .AddTransient<ILogger, Logger>();
    }

    public static IServiceCollection ConfigureLogger(this IServiceCollection services, IConfiguration config, params string[] sections)
    {
        return services.ConfigureOptions<LoggerOptions>(config, sections);
    }
}

