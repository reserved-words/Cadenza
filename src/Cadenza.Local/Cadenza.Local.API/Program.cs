using Cadenza.Local.API._Startup;

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