using Cadenza.Web.Info.Settings;

namespace Cadenza._Startup;

public static class Configuration
{
    public static WebAssemblyHostBuilder RegisterConfiguration(this WebAssemblyHostBuilder builder)
    {
        builder.Services
            .ConfigureSettings<InfoApiSettings>(builder.Configuration, "InfoApi")
            .ConfigureSettings<LastFmApiSettings>(builder.Configuration, "LastFmApi")
            .ConfigureSettings<LocalApiSettings>(builder.Configuration, "LocalApi")
            .ConfigureSettings<DatabaseApiSettings>(builder.Configuration, "DatabaseApi");

        return builder;
    }
}
