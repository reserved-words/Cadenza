using Cadenza.Common.Utilities.Json;
using Microsoft.AspNetCore.Mvc;

namespace Cadenza.Apps.API.Extensions;

internal static class Json
{
    internal static WebApplicationBuilder ConfigureJsonConverter(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JsonOptions>(options =>
        {
            JsonSerialization.SetOptions(options.JsonSerializerOptions);
        });

        return builder;
    }
}
