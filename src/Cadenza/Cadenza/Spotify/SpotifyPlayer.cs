using Cadenza.Common;

namespace Cadenza.Source.Spotify.Player;

public class SpotifyPlayer : ISourcePlayer
{
    private readonly IAudioPlayer _internalPlayer;
    public SpotifyPlayer(ISpotifyAuthHelper authHelper, IPlayerApi playerApi)
    {
        _internalPlayer = new InternalPlayer(authHelper, playerApi);
    }
    public LibrarySource Source => LibrarySource.Spotify;

    public async Task<TrackProgress> Pause()
    {
        return await _internalPlayer.Pause();
    }

    public async Task Play(string id)
    {
        await _internalPlayer.Play(id);
    }

    public async Task<TrackProgress> Resume()
    {
        return await _internalPlayer.Resume();
    }

    public async Task<TrackProgress> Stop()
    {
        return await _internalPlayer.Stop();
    }
}