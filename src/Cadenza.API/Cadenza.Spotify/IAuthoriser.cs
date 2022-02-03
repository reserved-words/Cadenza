namespace Cadenza.Spotify
{
    public interface IAuthoriser
    {
        Task<string> GetAuthHeader();
        Task<string> GetAuthUrl(string state, string redirectUri);
        Task<string> GetTokenUrl();
    }
}