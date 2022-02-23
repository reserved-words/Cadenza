﻿using Cadenza.Core;
using Cadenza.Core.Interfaces;
using Cadenza.Core.Model;
using Microsoft.Extensions.Options;

namespace Cadenza.Source.Local;

internal class LocalPlayer : ISourcePlayer
{
    private readonly IOptions<LocalApiSettings> _settings;
    private readonly IAudioPlayer _audioPlayer;

    public LocalPlayer(IAudioPlayer audioPlayer, IOptions<LocalApiSettings> settings)
    {
        _audioPlayer = audioPlayer;
        _settings = settings;
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
        var uri = string.Format(_settings.GetApiEndpoint(e => e.PlayTrackUrl), id);
        await _audioPlayer.Play(uri);
    }

    public async Task<TrackProgress> Stop()
    {
        return await _audioPlayer.Stop();
    }
}
