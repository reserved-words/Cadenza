
namespace Cadenza.Spotify.API.Interfaces
{
    public interface IApiToken
    {
        string GetAccessToken();
        void SetAccessToken(string accessToken);
    }
}
