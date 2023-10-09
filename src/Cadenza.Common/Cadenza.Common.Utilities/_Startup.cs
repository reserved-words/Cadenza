global using Cadenza.Common.Model;
global using Cadenza.Common.Utilities.Services;
global using Microsoft.Extensions.DependencyInjection;

using Cadenza.Common.Utilities.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Cadenza.Common.Utilities.Tests")]
[assembly: InternalsVisibleTo("TestConsoleApp")]

namespace Cadenza.Common.Utilities;

public static class _Startup
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        return services
            .AddTransient<IBase64Encoder, Base64Encoder>()
            .AddTransient<IImageConverter, ImageConverter>()
            .AddTransient<INameComparer, NameComparer>();
    }
}
