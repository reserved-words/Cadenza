global using Cadenza.Common.Utilities.Services;
global using Cadenza.Common.Interfaces.Utilities;
global using Cadenza.Common.Utilities.Exceptions;
global using Microsoft.Extensions.DependencyInjection;
global using Cadenza.Common.Domain.Model;


using FileAccess = Cadenza.Common.Utilities.Services.FileAccess;

namespace Cadenza.Common.Utilities;

public static class _Startup
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        return services
            .AddTransient<IBase64Converter, Base64Converter>()
            .AddTransient<IDateTime, CurrentDateTime>()
            .AddTransient<IFileAccess, FileAccess>()
            .AddTransient<IHasher, Hasher>()
            .AddTransient<IIdGenerator, IdGenerator>()
            .AddTransient<IJsonConverter, JsonConverter>()
            .AddTransient<INameComparer, NameComparer>()
            .AddTransient<IShuffler, Shuffler>();
    }

    public static IServiceCollection AddHttpHelper(this IServiceCollection services, Func<IServiceProvider, HttpClient> resolveClient)
    {
        return services.AddTransient<IHttpHelper>(sp => new HttpHelper(resolveClient(sp)));
    }
}
