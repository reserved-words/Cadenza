﻿global using Cadenza.Common.Domain.Model;
global using Cadenza.Common.Interfaces.Utilities;
global using Cadenza.Common.Utilities.Services;
global using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using FileAccess = Cadenza.Common.Utilities.Services.FileAccess;

[assembly:InternalsVisibleTo("Cadenza.Common.Utilities.Tests")]
[assembly: InternalsVisibleTo("TestConsoleApp")]

namespace Cadenza.Common.Utilities;

public static class _Startup
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        return services
            .AddTransient<IBase64Encoder, Base64Encoder>()
            .AddTransient<IFileAccess, FileAccess>()
            .AddTransient<IHasher, Hasher>()
            .AddTransient<IImageConverter, ImageConverter>()
            .AddTransient<IJsonConverter, JsonConverter>()
            .AddTransient<IHttpRequestSender, HttpRequestSender>()
            .AddTransient<INameComparer, NameComparer>()
            .AddTransient<IShuffler, Shuffler>();
    }

    public static IServiceCollection AddDefaultHttpHelper(this IServiceCollection services)
    {
        return services.AddTransient<IHttpHelper, DefaultHttpHelper>();
    }
}
