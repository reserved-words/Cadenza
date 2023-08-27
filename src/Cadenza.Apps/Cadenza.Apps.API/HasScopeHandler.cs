using Cadenza.Apps.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cadenza.Apps.API;

public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
    {
        var scopeClaim = context.User.GetClaim(ClaimType.Scope, requirement.Issuer);

        if (scopeClaim == null)
        {
            context.Fail(new AuthorizationFailureReason(this, "No scope claim for the relevant issuer"));
        }
        else if (!IncludesScope(scopeClaim, requirement))
        {
            context.Fail(new AuthorizationFailureReason(this, "Required scope not included in scope claim for the relevant issuer"));
        }
        else
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    private static bool IncludesScope(Claim scopeClaim, HasScopeRequirement requirement)
    {
        var scopes = scopeClaim.GetValueArray();
        return scopes.Any(s => s == requirement.Scope);
    }
}
