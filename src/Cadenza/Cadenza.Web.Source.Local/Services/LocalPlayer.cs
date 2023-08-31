﻿using Cadenza.Web.Common.Interfaces.Player;

namespace Cadenza.Web.Source.Local.Services;

internal class LocalPlayer : ISourcePlayer
{
    private readonly LocalApiSettings _settings;
    private readonly IAudioPlayer _audioPlayer;
    private readonly IBase64Encoder _base64Encoder;
    private readonly IUrl _url;

    public LocalPlayer(IAudioPlayer audioPlayer, IOptions<LocalApiSettings> settings, IUrl url, IBase64Encoder base64Encoder)
    {
        _audioPlayer = audioPlayer;
        _settings = settings.Value;
        _url = url;
        _base64Encoder = base64Encoder;
    }

    public LibrarySource Source => LibrarySource.Local;

    public async Task<TrackProgress> Resume()
    {
        return await _audioPlayer.Resume();
    }

    public async Task<TrackProgress> Pause()
    {
        return await _audioPlayer.Pause();
    }

    public async Task Play(string id)
    {
        var idBase64 = _base64Encoder.Encode(id);
        var uri = string.Format(_url.Build(_settings.BaseUrl, _settings.Endpoints.PlayTrackUrl), idBase64);
        await _audioPlayer.Play(uri);
    }

    public async Task<TrackProgress> Stop()
    {
        return await _audioPlayer.Stop();
    }
}
