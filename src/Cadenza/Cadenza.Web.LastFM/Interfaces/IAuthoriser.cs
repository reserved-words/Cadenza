namespace Cadenza.Web.LastFM.Interfaces;

internal interface IAuthoriser
{
    Task<string> GetAuthUrl(string redirectUri);
    Task<string> CreateSession(string token);
}
