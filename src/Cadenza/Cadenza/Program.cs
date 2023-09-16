namespace Cadenza;

using Cadenza.State;
using Cadenza.State.Effects;
using Cadenza.State.Middleware;
using Cadenza.State.Store;
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
            .AddMiddleware<LoggingMiddleware>());

        builder.Services.RegisterDependencies(builder.Configuration);

        var audience = builder.Configuration["AppAuthentication:Audience"];
        var scopeDatabase = builder.Configuration["AppAuthentication:Scopes:Database"];
        var scopeLocal = builder.Configuration["AppAuthentication:Scopes:Local"];

        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("AppAuthentication", options.ProviderOptions);
            options.ProviderOptions.ResponseType = "code";
            options.ProviderOptions.AdditionalProviderParameters.Add("audience", audience);
            options.ProviderOptions.DefaultScopes.Add(scopeDatabase);
            options.ProviderOptions.DefaultScopes.Add(scopeLocal);
        });


        await builder.Build().RunAsync();
    }
}
