namespace Cadenza.Source.Spotify.Api.Model.Auth;

public class RefreshTokenRequest
{
    public string grant_type { get; set; }
    public string refresh_token { get; set; }

    public Dictionary<string, string> AsPostData()
    {
        return new Dictionary<string, string>
        {
            { "grant_type", grant_type },
            { "refresh_token", refresh_token }
        };
    }
}
