namespace Cadenza.Source.Spotify.Api.Model.Auth;

public class CreateSessionResponse
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public string scope { get; set; }
    public int expires_in { get; set; } // seconds
    public string refresh_token { get; set; }
}
