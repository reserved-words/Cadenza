using Cadenza.Apps.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Cadenza.Apps.API;

public class IsValidUserHandler : AuthorizationHandler<IsValidUserRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsValidUserRequirement requirement)
    {
        if (IsService(context, requirement))
        {
            context.Succeed(requirement);
        }
        else if (IsValidEmail(context, requirement))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail(new AuthorizationFailureReason(this, "Not a valid user"));
        }

        return Task.CompletedTask;
    }

    private static bool IsService(AuthorizationHandlerContext context, IsValidUserRequirement requirement)
    {
        var gtyClaim = context.User.GetClaim(ClaimType.GrantType, requirement.Issuer);

        return gtyClaim != null && gtyClaim.Value == ClaimValue.ClientCredentials;
    }

    private static bool IsValidEmail(AuthorizationHandlerContext context, IsValidUserRequirement requirement)
    {
        var emailClaim = context.User.GetClaim(ClaimType.Email, requirement.Issuer);

        if (emailClaim == null)
            return false;

        if (!requirement.ValidUsers.Contains(emailClaim.Value))
            return false;

        return true;
    }
}
