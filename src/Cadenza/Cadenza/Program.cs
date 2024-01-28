namespace Cadenza;

using Cadenza.Web.Actions.Effects;
using Cadenza.Web.State.Store;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RegisterComponents();

        builder.RegisterConfiguration();

        var currentAssembly = typeof(Program).Assembly;
        var stateAssembly = typeof(PlayStatusState).Assembly;
        var effectsAssembly = typeof(RecentPlayHistoryEffects).Assembly;
        builder.Services
            .AddFluxor(options => options.ScanAssemblies(currentAssembly, stateAssembly, effectsAssembly)
            //.AddMiddleware<LoggingMiddleware>()
        );

        //builder.Logging.AddFilter((category, level) =>
        //{
        //    return category == null || !category.Contains("System.Net.Http.HttpClient");
        //});

        builder.Services.RegisterDependencies(builder.Configuration);

        await builder.Build().RunAsync();
    }
}
