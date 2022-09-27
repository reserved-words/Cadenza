using Cadenza.Utilities.Implementations;
using Cadenza.Utilities.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FileAccess = Cadenza.Utilities.Implementations.FileAccess;

namespace Cadenza.Utilities;

public static class _Startup
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        return services
            .AddTransient<IBase64Converter, Base64Converter>()
            .AddTransient<IDateTime, CurrentDateTime>()
            .AddTransient<IFileAccess, FileAccess>()
            .AddTransient<IHasher, Hasher>()
            .AddTransient<IHttpHelper, HttpHelper>()
            .AddTransient<IIdGenerator, IdGenerator>()
            .AddTransient<IJsonConverter, JsonConverter>()
            .AddTransient<INameComparer, NameComparer>()
            .AddTransient<IShuffler, Shuffler>();
    }

    public static IServiceCollection AddHttpClient(this IServiceCollection services, HttpClient client)
    {
        return services.AddTransient(sp => client)
            .AddTransient<IHttpHelper, HttpHelper>();
    }
}
