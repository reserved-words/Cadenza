using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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