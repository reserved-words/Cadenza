
using Cadenza.SyncService._Startup;
using Cadenza.SyncService;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.RegisterConfiguration();
        services.RegisterDependencies();
        services.AddHostedService<Worker>();
    })
    .UseWindowsService();

builder.Build().Run();