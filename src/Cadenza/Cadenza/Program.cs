namespace Cadenza;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = await WebAssemblyHostBuilder.CreateDefault(args)
            .RegisterComponents()
            .RegisterDependencies()
            .RegisterConfiguration();

        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("Authentication", options.ProviderOptions);
            options.ProviderOptions.ResponseType = "code";
        });

        await builder.Build().RunAsync();
    }
}
