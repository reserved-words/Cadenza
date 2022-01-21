namespace Cadenza.Source.Spotify;

public class SpotifyPlayer : ISourcePlayer
{
    private readonly IAudioPlayer _internalPlayer;

    public SpotifyPlayer(IHttpHelper httpClient, ISpotifyApiConfig config)
    {
        var api = new SpotifyApi(httpClient, config);
        var playerApi = new SpotifyPlayerApi(api);
        _internalPlayer = new InternalPlayer(playerApi);
    }
    public LibrarySource Source => LibrarySource.Spotify;

    public async Task<int> Pause()
    {
        return await _internalPlayer.Pause();
    }

    public async Task Play(string id)
    {
        await _internalPlayer.Play(id);
    }

    public async Task<int> Resume()
    {
        return await _internalPlayer.Resume();
    }

    public async Task<int> Stop()
    {
        return await _internalPlayer.Stop();
    }
}