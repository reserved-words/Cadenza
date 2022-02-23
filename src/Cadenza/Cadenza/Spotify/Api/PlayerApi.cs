using Cadenza.Core.Model;

namespace Cadenza.Source.Spotify.Player;

public class PlayerApi : IPlayerApi
{    
    // Add to config
    private const string PauseUrlFormat = "https://api.spotify.com/v1/me/player/pause?device_id={0}";
    private const string PlayUrlFormat = "https://api.spotify.com/v1/me/player/play?device_id={0}";

    private readonly IApiHelper _api;
    private readonly IDeviceHelper _devicesApi;
    private readonly IProgressApi _progressApi;

    public PlayerApi(IApiHelper api, IDeviceHelper devicesApi, IProgressApi progressApi)
    {
        _api = api;
        _devicesApi = devicesApi;
        _progressApi = progressApi;
    }

    public async Task<TrackProgress> Stop(string accessToken)
    {
        var progress = await _progressApi.GetProgress(accessToken);
        await TryPut(PauseUrlFormat, accessToken);
        return progress;
    }

    public async Task Play(string trackId, string accessToken)
    {
        await TryPut(PlayUrlFormat, accessToken, GetTrackData(trackId));
    }

    public async Task<TrackProgress> Pause(string accessToken)
    {
        await TryPut(PauseUrlFormat, accessToken);
        return await _progressApi.GetProgress(accessToken);
    }

    public async Task<TrackProgress> Resume(string accessToken)
    {
        await TryPut(PlayUrlFormat, accessToken);
        return await _progressApi.GetProgress(accessToken);
    }

    private async Task TryPut(string urlFormat, string accessToken, object data = null)
    {
        try
        {
            var deviceId = await _devicesApi.GetDeviceId(accessToken, false);
            await _api.Put(string.Format(urlFormat, deviceId), accessToken, data);
        }
        catch (DeviceNotFoundException)
        {
            var deviceId = await _devicesApi.GetDeviceId(accessToken, true);
            await _api.Put(string.Format(urlFormat, deviceId), accessToken, data);
        }
    }

    private static object GetTrackData(string trackId)
    {
        var split = trackId.Split("|");
        var spotifyId = $"spotify:track:{split[0]}";
        return new
        {
            uris = new string[] { spotifyId }
        };
    }
}
