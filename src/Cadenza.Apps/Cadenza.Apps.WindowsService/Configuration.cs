namespace Cadenza.Apps.WindowsService;

public static class Configuration
{
    public static IConfiguration RegisterConfiguration(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .AddEnvironmentJsonFile()
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton(configuration);

        return configuration;
    }

    private static IConfigurationBuilder AddEnvironmentJsonFile(this IConfigurationBuilder builder)
    {
        var env = Environment.GetEnvironmentVariable("ENVIRONMENT");

        if (string.IsNullOrWhiteSpace(env))
            return builder;

        return builder.AddJsonFile($"appsettings.{env}.json", false);
    }
}
