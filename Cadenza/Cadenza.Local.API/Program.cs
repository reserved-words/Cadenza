using Cadenza.Local.API;

var builder = WebApplication.CreateBuilder(args)
    .BuildConfiguration()
    .RegisterDependencies()
    .DefineCors()
    .AddDocumentation();

var app = builder.Build()
    .ApplyCors()
    .UseDocumentation()
    .MapRoutes()
    .GenerateDocumentation();

app.Run();