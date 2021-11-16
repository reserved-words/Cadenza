namespace Cadenza.Source.Spotify;

internal class SpotifyPlayerApi : ISpotifyPlayerApi
{
    private const string PauseUrlFormat = "https://api.spotify.com/v1/me/player/pause?device_id={0}";
    private const string PlayUrlFormat = "https://api.spotify.com/v1/me/player/play?device_id={0}";
    private const string PlayStateUrl = "https://api.spotify.com/v1/me/player";

    private readonly ISpotifyApi _api;

    public SpotifyPlayerApi(ISpotifyApi api)
    {
        _api = api;
    }

    public async Task<SpotifyApiPlayState> GetPlayState()
    {
        return await _api.Get<SpotifyApiPlayState>(PlayStateUrl);
    }

    public async Task Play(string trackId = null)
    {
        var data = GetPlayData(trackId);
        await _api.Put(PlayUrlFormat, data);
    }

    public async Task Pause()
    {
        await _api.Put(PauseUrlFormat);
    }

    private object GetPlayData(string trackId)
    {
        return trackId != null
            ? new
            {
                uris = new string[] { trackId }
            }
            : null;
    }
}