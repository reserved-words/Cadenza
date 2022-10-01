namespace Cadenza;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = await WebAssemblyHostBuilder.CreateDefault(args)
            .RegisterComponents()
            .RegisterDependencies()
            .RegisterConfiguration();

        await builder.Build().RunAsync();
    }
}
