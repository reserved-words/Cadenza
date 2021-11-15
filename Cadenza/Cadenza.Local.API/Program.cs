using Cadenza.Local.API;

var builder = WebApplication.CreateBuilder(args)
    .BuildConfiguration()
    .RegisterDependencies()
    .DefineCors();

var app = builder.Build()
    .ApplyCors()
    .MapRoutes();

app.Run();