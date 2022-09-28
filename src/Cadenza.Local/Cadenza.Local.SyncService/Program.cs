
var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.RegisterConfiguration();
        services.RegisterDependencies();
        services.AddHostedService<Worker>();
    })
    .UseWindowsService();

builder.Build().Run();