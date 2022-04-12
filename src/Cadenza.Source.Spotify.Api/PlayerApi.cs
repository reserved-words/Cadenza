using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Internal;
using Cadenza.Source.Spotify.Api.Model.Player;

namespace Cadenza.Source.Spotify.Api;

internal class PlayerApi : IPlayerApi
{
    // Add to config
    private const string PauseUrlFormat = "https://api.spotify.com/v1/me/player/pause?device_id={0}";
    private const string PlayUrlFormat = "https://api.spotify.com/v1/me/player/play?device_id={0}";
    private const string PlayStateUrl = "https://api.spotify.com/v1/me/player";

    private readonly IApiHelper _api;

    public PlayerApi(IApiHelper api)
    {
        _api = api;
    }

    public async Task Pause(string deviceId)
    {
        await _api.Put(string.Format(PauseUrlFormat, deviceId));
    }

    public async Task Play(string deviceId, string trackId = null)
    {
        //try
        //{

        //}
        //catch (UnauthorizedApiException)
        //{
        //    accessToken = await CreateNewSession();
        //    await _playerApi.Play(trackId);
        //}

        var data = trackId == null ? null : GetTrackData(trackId);
        await _api.Put(string.Format(PlayUrlFormat, deviceId), data);
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

    public async Task<SpotifyApiPlayState> GetPlayState()
    {
        var response = await _api.Get<SpotifyApiPlayState>(PlayStateUrl);
        return response.Data;
    }

    //private async Task TryPut(string urlFormat, string accessToken, object data = null)
    //{
    //    try
    //    {
    //        var deviceId = await _devicesApi.GetDeviceId(accessToken, false);
    //        await _api.Put(string.Format(urlFormat, deviceId), accessToken, data);
    //    }
    //    catch (DeviceNotFoundException)
    //    {
    //        var deviceId = await _devicesApi.GetDeviceId(accessToken, true);
    //        await _api.Put(string.Format(urlFormat, deviceId), accessToken, data);
    //    }
    //}
}
