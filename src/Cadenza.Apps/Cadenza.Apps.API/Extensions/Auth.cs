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
                c.Authority = authSettings.FullDomain;
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = authSettings.Audience,
                    ValidIssuer = authSettings.FullDomain
                };
            });

        builder.Services.AddAuthorization(options => options.AddPolicy(authSettings.Scope, policy =>
        {
            policy.Requirements.Add(new HasScopeRequirement(authSettings.Scope, authSettings.FullDomain));
            policy.Requirements.Add(new IsValidUserRequirement(authSettings.ValidUsers, authSettings.FullDomain));
        }));

        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        builder.Services.AddSingleton<IAuthorizationHandler, IsValidUserHandler>();

        return builder;
    }

    internal static void RequireAuthorization(this ControllerActionEndpointConventionBuilder builder, IConfiguration config, string configSectionName)
    {
        var scope = config[$"{configSectionName}:Scope"];
        builder.RequireAuthorization(scope);
    }

    private static ApiAuthSettings GetAuthSettings(this WebApplicationBuilder builder, string authConfigSectionName)
    {
        return builder.Configuration.GetSection(authConfigSectionName).Get<ApiAuthSettings>();
    }
}
