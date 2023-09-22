namespace Cadenza.Web.Common.Model;

public class AuthenticationSettings
{
    public string Authority { get; set; }
    public string ClientId { get; set; }
    public string Audience { get; set; }
    public string LogoutRedirect { get; set; }
}
