using Cadenza.Spotify.API.Interfaces;

namespace Cadenza.Spotify.API;

internal class ApiToken : IApiToken
{
    private string _token;

    public string GetAccessToken()
    {
        return _token;
    }

    public void SetAccessToken(string accessToken)
    {
        _token = accessToken;
    }
}
