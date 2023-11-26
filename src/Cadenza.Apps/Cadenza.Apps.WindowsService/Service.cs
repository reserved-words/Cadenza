global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Events;

namespace Cadenza.Apps.WindowsService;

public static class Service
{
    public static IHostBuilder CreateBuilder(string[] args, Action<IServiceCollection> registerDependencies)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                registerDependencies(services);
            })
            .UseWindowsService()
            .UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Verbose)
                .WriteTo.File(ctx.Configuration.LogFilePath(), rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information));
    }
}