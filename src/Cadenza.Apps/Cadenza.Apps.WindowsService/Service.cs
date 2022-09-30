global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Options;

global using Cadenza.Apps.WindowsService.Interfaces;
global using Cadenza.Apps.WindowsService.Settings;

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
            .UseWindowsService();
    }
}