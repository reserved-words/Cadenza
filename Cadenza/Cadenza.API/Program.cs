using Cadenza.API;

var builder = WebApplication.CreateBuilder(args)
    .RegisterDependencies()
    .RegisterConfiguration()
    .RegisterCorsPolicies()
    .RegisterDocumentation();

// May need to add documentation before mapping routes

var app = builder.Build()
    .AddCors()
    .AddDocumentation()
    .AddRoutes()
    .AddDocumentationUI();

app.Run();