using Cadenza.Core.Interfaces;
using Cadenza.Core.Model;

namespace Cadenza.Source.Spotify.Player;

public class InternalPlayer : IAudioPlayer
{
    private readonly ISpotifyAuthHelper _authHelper;
    private readonly IPlayerApi _playerApi;

    public InternalPlayer(ISpotifyAuthHelper authHelper, IPlayerApi playerApi)
    {
        _authHelper = authHelper;
        _playerApi = playerApi;
    }

    public async Task Play(string trackId)
    {
        var accessToken = await GetAccessToken();

        try
        {
            await _playerApi.Play(trackId, accessToken);

        }
        catch (UnauthorizedApiException)
        {
            accessToken = await CreateNewSession();
            await _playerApi.Play(trackId, accessToken);
        }
    }

    public async Task<TrackProgress> Pause()
    {
        var accessToken = await GetAccessToken();

        try
        {
            return await _playerApi.Pause(accessToken);

        }
        catch (UnauthorizedApiException)
        {
            accessToken = await CreateNewSession();
            return await _playerApi.Pause(accessToken);
        }
    }

    public async Task<TrackProgress> Resume()
    {
        var accessToken = await GetAccessToken();
        try
        {
            return await _playerApi.Resume(accessToken);
        }
        catch (UnauthorizedApiException)
        {
            accessToken = await CreateNewSession();
            return await _playerApi.Resume(accessToken);
        }
    }

    public async Task<TrackProgress> Stop()
    {
        var accessToken = await GetAccessToken();

        try
        {
            return await _playerApi.Stop(accessToken);
        }
        catch (UnauthorizedApiException)
        {
            accessToken = await CreateNewSession();
            return await _playerApi.Stop(accessToken);
        }
    }

    private async Task<string> CreateNewSession()
    {
        return await _authHelper.CreateSession(CancellationToken.None);
    }

    private async Task<string> GetAccessToken()
    {
        return await _authHelper.GetAccessToken(CancellationToken.None);
    }
}
