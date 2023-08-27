using System.Security.Claims;

namespace Cadenza.Apps.API.Extensions;

internal static class Claims
{
    internal static Claim GetClaim(this ClaimsPrincipal user, string type, string issuer)
    {
        return user.FindFirst(c => c.Type == type && c.Issuer == issuer);
    }

    internal static string[] GetValueArray(this Claim claim)
    {
        return claim.Value.Split(' ');
    }
}
