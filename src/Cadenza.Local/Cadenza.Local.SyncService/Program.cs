using Cadenza.Apps.WindowsService;

var builder = Service.CreateBuilder(args, services =>
{
    services.RegisterConfiguration();
    services.RegisterDependencies();
});

builder.Build().Run();