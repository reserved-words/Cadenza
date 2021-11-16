namespace Cadenza.Local.API
{
    public static class Documentation
    {
        public static WebApplicationBuilder AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            return builder;
        }

        public static WebApplication UseDocumentation(this WebApplication app)
        {
            app.UseSwagger();
            return app;
        }

        public static WebApplication GenerateDocumentation(this WebApplication app) 
        { 
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
