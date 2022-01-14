using Cadenza.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Common;

public static class Startup
{
    public static IServiceCollection AddCommonUtilities(this IServiceCollection services)
    {
        services
            .AddTransient<IBase64Converter, Base64Converter>()
            .AddTransient<IDateTime, CurrentDateTime>()
            .AddTransient<IHasher, Hasher>()
            .AddTransient<IHttpHelper, HttpHelper>()
            .AddTransient<IListComparer, ListComparer>()
            .AddTransient<IRandomGenerator, RandomGenerator>()
            .AddTransient<IShuffler, Shuffler>();
        
        return services
            .AddTransient<IIdGenerator, IdGenerator>()
            .AddTransient<ILongRunningTaskService, LongRunningTaskService>()
            .AddTransient<IMerger, Merger>()
            .AddTransient<INameComparer, NameComparer>()
            .AddTransient<IValueMerger, ValueMerger>();
    }

    public static IServiceCollection AddHttpClient(this IServiceCollection services, System.Net.Http.HttpClient client)
    {
        return services.AddTransient(sp => client)
            .AddTransient<IHttpHelper, HttpHelper>();
    }

    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        return services.AddTransient<System.Net.Http.HttpClient>()
            .AddTransient<IHttpHelper, HttpHelper>()
            .AddTransient<ILogger, Logger>();
    }

    public static IServiceCollection AddLogger(this IServiceCollection services, System.Net.Http.HttpClient client)
    {
        return services.AddTransient(sp => client)
            .AddTransient<IHttpHelper, HttpHelper>()
            .AddTransient<ILogger, Logger>();
    }

    public static IServiceCollection ConfigureLogger(this IServiceCollection services, IConfiguration config, params string[] sections)
    {
        return services.ConfigureOptions<LoggerOptions>(config, sections);
    }
}

