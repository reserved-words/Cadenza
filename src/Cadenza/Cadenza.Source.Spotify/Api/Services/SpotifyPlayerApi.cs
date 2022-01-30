﻿using Cadenza.Core;

namespace Cadenza.Source.Spotify;

public class SpotifyPlayerApi : ISpotifyPlayerApi
{
    private const string PauseUrlFormat = "https://api.spotify.com/v1/me/player/pause?device_id={0}";
    private const string PlayUrlFormat = "https://api.spotify.com/v1/me/player/play?device_id={0}";
    private const string PlayStateUrl = "https://api.spotify.com/v1/me/player";

    private readonly ISpotifyInterop _interop;
    private readonly ISpotifyApi _api;

    public SpotifyPlayerApi(ISpotifyApi api, ISpotifyInterop interop)
    {
        _api = api;
        _interop = interop;
    }

    public async Task<SpotifyApiPlayState> GetPlayState()
    {
        var response = await _api.Get<SpotifyApiPlayState>(PlayStateUrl);
        return response.Data;
    }

    public async Task Play(string trackId = null)
    {
        var data = GetPlayData(trackId);
        var response = await _api.Put(PlayUrlFormat, data);

        if (response.Success)
            return;

        if (response.Error.Status == 404 && response.Error.Message == "Device not found")
        {
            var resolved = await _interop.DeviceNotFound();

            if (!resolved)
            {
                throw new ConnectorException(Connector.Spotify, ConnectorError.PlaybackFailure, "Device not found");
            }
            else
            {
                // Try again to play
            }
        }
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
                uris = new string[] { GetSpotifyId(trackId) }
            }
            : null;
    }

    private string GetSpotifyId(string trackId)
    {
        var split = trackId.Split("|");
        return $"spotify:track:{split[0]}";
    }
}