using Cadenza.Core.Interfaces;
using Cadenza.Core.Model;
using Cadenza.Domain;
using Cadenza.Source.Spotify.Interfaces;


namespace Cadenza.Source.Spotify.Services;

internal class SpotifyPlayer : ISourcePlayer
{
    private readonly IPlayerService _playerApi;

    public SpotifyPlayer(IPlayerService playerApi)
    {
        _playerApi = playerApi;
    }

    public LibrarySource Source => LibrarySource.Spotify;

    public async Task Play(string trackId)
    {
        await _playerApi.Play(trackId);
    }

    public async Task<TrackProgress> Pause()
    {
        return await _playerApi.Pause();
    }

    public async Task<TrackProgress> Resume()
    {
        return await _playerApi.Resume();
    }

    public async Task<TrackProgress> Stop()
    {
        return await _playerApi.Stop();
    }
}