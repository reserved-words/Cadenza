namespace Cadenza.API;

public static class Configuration
{
    public static WebApplicationBuilder BuildConfiguration(this WebApplicationBuilder builder)
    {
        var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH")
            ?? "appsettings.json";

        builder.Configuration.AddJsonFile(settingsPath);
        return builder;
    }
}
