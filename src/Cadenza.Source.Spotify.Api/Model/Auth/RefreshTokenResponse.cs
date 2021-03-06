namespace Cadenza.Source.Spotify.Api.Model.Auth;

public class RefreshTokenResponse
{
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public string scope { get; set; }
    public int expires_in { get; set; } // seconds
}
