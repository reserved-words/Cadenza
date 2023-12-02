﻿using Cadenza.Web.Common.Settings;

namespace Cadenza._Startup;

public static class Configuration
{
    public static WebAssemblyHostBuilder RegisterConfiguration(this WebAssemblyHostBuilder builder)
    {
        builder.Services
            .ConfigureSettings<AppSettings>(builder.Configuration, "AppSettings")
            .ConfigureSettings<LocalApiSettings>(builder.Configuration, "LocalApi")
            .ConfigureSettings<DatabaseApiSettings>(builder.Configuration, "DatabaseApi")
            .ConfigureSettings<AuthenticationSettings>(builder.Configuration, "AppAuthentication");

        return builder;
    }
}
