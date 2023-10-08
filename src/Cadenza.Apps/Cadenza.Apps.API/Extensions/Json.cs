using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cadenza.Apps.API.Extensions;

internal static class Json
{
    internal static WebApplicationBuilder ConfigureJsonSerialization(this WebApplicationBuilder builder, Action<JsonSerializerOptions> setOptions)
    {
        builder.Services.Configure<JsonOptions>(options =>
        {
            setOptions(options.JsonSerializerOptions);
        });

        return builder;
    }
}
