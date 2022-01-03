
namespace Cadenza.Source.Spotify;

public class SpotifyOverridesSettings : ApiOptions<SpotifyOverridesEndpoints>
{
}

public class SpotifyOverridesEndpoints
{
    public string AddOverride { get; set; }
    public string GetOverrides { get; set; }
    public string RemoveOverride { get; set; }
}