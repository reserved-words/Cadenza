
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