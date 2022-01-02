using Cadenza.Local.API;

var builder = WebApplication.CreateBuilder(args)
    .RegisterDependencies()
    .RegisterConfiguration()
    .RegisterCorsPolicies()
    .RegisterDocumentation();

var app = builder.Build()
    .AddCors()
    .AddDocumentation()
    .AddRoutes()
    .AddDocumentationUI();

app.Run();