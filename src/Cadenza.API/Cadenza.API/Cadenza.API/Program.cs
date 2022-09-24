using Cadenza.API._Startup;

try
{
    var builder = WebApplication.CreateBuilder(args)
        .RegisterDependencies()
        .RegisterConfiguration()
        .RegisterCorsPolicies()
        .RegisterDocumentation();

    var app = builder.Build();

    app.AddCors();
    app.AddDocumentation();
    app.MapControllers();
    app.AddDocumentationUI();

    app.Run();
}
catch (Exception ex)
{

    throw;
}