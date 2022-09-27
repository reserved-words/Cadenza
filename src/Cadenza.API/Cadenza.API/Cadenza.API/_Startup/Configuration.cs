namespace Cadenza.API._Startup;

public static class Configuration
{
    public static WebApplicationBuilder RegisterConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile(GetSettingsPath());
        
        builder.Services
            .ConfigureLastFM(builder.Configuration, "LastFm")
            .ConfigureJsonLibrary(builder.Configuration, "LibraryPaths");

        return builder;
    }

    private static string GetSettingsPath()
    {
        return Environment.GetEnvironmentVariable("SETTINGS_PATH") ?? "appsettings.json";
    }
}
