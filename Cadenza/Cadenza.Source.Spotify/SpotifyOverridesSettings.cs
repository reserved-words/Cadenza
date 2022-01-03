
namespace Cadenza.Source.Spotify;

public class SpotifyOverridesSettings
{
    public string BaseUrl { get; set; }
    public SpotifyOverridesEndpoints Endpoints { get; set; }

    public class SpotifyOverridesEndpoints
    {
        public string AddOverride { get; set; }
        public string GetOverrides { get; set; }
        public string RemoveOverride { get; set; }
    }
}
