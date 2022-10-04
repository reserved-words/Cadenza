global using Cadenza.Apps.WindowsService.Interfaces;
global using Cadenza.Apps.WindowsService.Settings;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Options;

using Serilog;

namespace Cadenza.Apps.WindowsService;

public static class Service
{
    public static IHostBuilder CreateBuilder(string[] args, Action<IServiceCollection> registerDependencies)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                registerDependencies(services);
                services.AddHostedService<Worker>();
            })
            .UseWindowsService()
            .UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)
                .WriteTo.Console()
                .WriteTo.File(ctx.Configuration.LogFilePath(), rollingInterval: RollingInterval.Day));
    }
}