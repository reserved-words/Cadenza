using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Cadenza.Local.API._Startup;

public static class Cors
{
    public static WebApplicationBuilder RegisterCorsPolicies(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(opts => builder.SetUpCorsOptions(opts));
        return builder;
    }

    public static WebApplication AddCors(this WebApplication app)
    {
        var allowedClients = GetAllowedClients(app.Configuration);
        allowedClients.ForEach(cli => app.UseCors(cli.Name));
        return app;
    }

    private static void SetUpCorsOptions(this WebApplicationBuilder builder, CorsOptions opts)
    {
        var allowedClients = GetAllowedClients(builder.Configuration);

        allowedClients.ForEach(cli => opts.AddPolicy(
            name: cli.Name,
            builder =>
            {
                builder.WithOrigins(cli.Origin)
                    .WithMethods("GET", "POST", "OPTIONS")
                    .WithHeaders("content-type");
            }));
    }

    private static List<AllowedClient> GetAllowedClients(IConfiguration configuration)
    {
        return configuration
            .GetSection("AllowedClients")
            .Get<AllowedClient[]>()
            .ToList();
    }

    private class AllowedClient
    {
        public string Name { get; set; }
        public string Origin { get; set; }
    }
}
