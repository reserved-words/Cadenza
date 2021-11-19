using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Cadenza.API;

public static class CorsPolicies
{
    public static WebApplicationBuilder DefineCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(opts => SetUpCorsOptions(builder, opts));
        return builder;
    }

    public static WebApplication ApplyCors(this WebApplication app)
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
                    .WithMethods("GET", "POST")
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
