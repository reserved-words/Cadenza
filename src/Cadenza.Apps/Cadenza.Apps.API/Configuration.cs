namespace Cadenza.Apps.API;

public static class Configuration
{
    public static WebApplicationBuilder RegisterConfiguration(this WebApplicationBuilder builder)
    {
        var path = GetPath();
        builder.Configuration.AddJsonFile(path);
        return builder;
    }

    private static string GetPath()
    {
        return Environment.GetEnvironmentVariable("SETTINGS_PATH") ?? "appsettings.json";
    }
}
