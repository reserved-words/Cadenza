using Cadenza.Apps.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Cadenza.Apps.API.Extensions;

internal static class Auth
{
    internal static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder, string authConfigSectionName)
    {
        var authSettings = builder.GetAuthSettings(authConfigSectionName);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
            {
                c.Authority = authSettings.Domain;
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = authSettings.Audience,
                    ValidIssuer = authSettings.Domain
                };
            });

        builder.Services.AddAuthorization(options => options.AddPolicy(authSettings.Scope, policy => policy.Requirements.Add(new
            HasScopeRequirement(authSettings.Scope, authSettings.Domain))));

        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        return builder;
    }

    internal static void RequireAuthorization(this ControllerActionEndpointConventionBuilder builder, IConfiguration config)
    {
        var scope = config["ApiAuthentication:Scope"];
        builder.RequireAuthorization(scope);
    }

    private static (string Domain, string Audience, string Scope) GetAuthSettings(this WebApplicationBuilder builder, string authConfigSectionName)
    {
        var domain = $"https://{builder.Configuration[$"{authConfigSectionName}:Domain"]}/";
        var audience = builder.Configuration[$"{authConfigSectionName}:Audience"];
        var scope = builder.Configuration[$"{authConfigSectionName}:Scope"];

        return (domain, audience, scope);
    }
}
