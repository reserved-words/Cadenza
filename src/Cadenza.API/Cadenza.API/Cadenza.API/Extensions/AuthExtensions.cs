namespace Cadenza.API.Extensions;

internal static class AuthExtensions
{
    internal static string GetUsername(this HttpContext httpContext)
    {
        return httpContext.User.Claims.Single(c => c.Type == "cadenza/email").Value;
    }
}
