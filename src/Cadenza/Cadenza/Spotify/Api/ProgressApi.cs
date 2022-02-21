using Cadenza.Common;

namespace Cadenza.Source.Spotify.Player;

public class ProgressApi : IProgressApi
{
    private const string PlayStateUrl = "https://api.spotify.com/v1/me/player";

    private readonly IApiHelper _api;

    public ProgressApi(IApiHelper api)
    {
        _api = api;
    }

    public async Task<TrackProgress> GetProgress(string accessToken)
    {
        var response = await _api.Get<SpotifyApiPlayState>(PlayStateUrl, accessToken);
        var playState = response.Data;

        if (playState == null || playState.item == null)
            return new TrackProgress(-1, -1);

        return new TrackProgress(
            MillisecondsToSeconds(playState.progress_ms),
            MillisecondsToSeconds(playState.item.duration_ms));
    }

    private static int MillisecondsToSeconds(int? milliseconds)
    {
        return (milliseconds ?? 0) / 1000;
    }
}
