namespace Cadenza.Apps.API;

public static class Cors
{
    public static WebApplicationBuilder SetCorsPolicy(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(opts => 
        {
            var allowedClients = GetAllowedClients(builder.Configuration);

            var allowedOrigins = allowedClients.Select(c => c.Origin).ToArray();

            var cli = allowedClients.Single();

            opts.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(allowedOrigins)
                    .WithMethods("GET", "POST", "PUT", "OPTIONS", "DELETE")
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        return builder;
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
