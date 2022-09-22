namespace Cadenza.API._Startup;

public static class Configuration
{
    public static WebApplicationBuilder RegisterConfiguration(this WebApplicationBuilder builder)
    {
        var settingsPath = Environment.GetEnvironmentVariable("SETTINGS_PATH") ?? "appsettings.json";
        builder.Configuration.AddJsonFile(settingsPath);
        builder.Services.ConfigureLastFM(builder.Configuration, "LastFm");
        return builder;
    }
}
