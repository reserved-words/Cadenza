namespace Cadenza.Source.Spotify.Api.Model.Auth;

public class CreateSessionRequest
{
    public string code { get; set; }
    public string redirect_uri { get; set; }
    public string grant_type { get; set; }

    public Dictionary<string, string> AsPostData()
    {
        return new Dictionary<string, string>
        {
            { "code", code },
            { "redirect_uri", redirect_uri },
            { "grant_type", grant_type }
        };
    }
}