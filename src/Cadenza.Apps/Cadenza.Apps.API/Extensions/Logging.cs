using Serilog;

namespace Cadenza.Apps.API.Extensions;

internal static class Logging
{
    internal static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(ctx.Configuration.LogFilePath(), rollingInterval: RollingInterval.Day));

        return builder;
    }
}
