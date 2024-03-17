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

        builder.RegisterConfiguration(); ;

        builder.Logging.AddFilter((category, level) =>
        {
            return level >= LogLevel.Warning || !(category?.Contains("System.Net.Http.HttpClient") == true);
        });

        var currentAssembly = typeof(Program).Assembly;
        var stateAssembly = typeof(PlayStatusState).Assembly;
        var effectsAssembly = typeof(RecentPlayHistoryEffects).Assembly;
        builder.Services.AddFluxor(options => options.ScanAssemblies(currentAssembly, stateAssembly, effectsAssembly));

        builder.Services.RegisterDependencies(builder.Configuration);

        await builder.Build().RunAsync();
    }
}
