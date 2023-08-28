using Microsoft.AspNetCore.Authorization;

namespace Cadenza.Apps.API;

public class IsValidUserRequirement : IAuthorizationRequirement
{
    public string Issuer { get; }
    public string[] ValidUsers { get; }

    public IsValidUserRequirement(string[] validUsers, string issuer)
    {
        Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        ValidUsers = validUsers ?? throw new ArgumentNullException(nameof(validUsers));
    }
}
