using Cadenza.Common;
using Cadenza.Core;
using Cadenza.Utilities;
using Microsoft.JSInterop;

namespace Cadenza.Source.Spotify.Player;

public class SpotifyPlayer : ISourcePlayer
{
    private readonly IAudioPlayer _internalPlayer;
    private readonly ISpotifyInterop _interop;
    private readonly IPlayerApi _player;
    private readonly IStoreGetter _storeGetter;
    private readonly IStoreSetter _storeSetter;

    public SpotifyPlayer(IStoreGetter storeGetter, IStoreSetter storeSetter, IHttpHelper httpClient, ISpotifyApiConfig config, 
        ISpotifyInterop interop, IJSRuntime js, IPlayerApi player)
    {
        var errorHandler = new SpotifyInterop(js);
        var api = new ApiHelper(httpClient, config);
        var playerApi = new PlayerApi(api, errorHandler);
        _internalPlayer = new InternalPlayer(playerApi);
        _interop = interop;
        _storeGetter = storeGetter;
        _storeSetter = storeSetter;
        _player = player;
    }
    public LibrarySource Source => LibrarySource.Spotify;

    public async Task<TrackProgress> Pause()
    {
        return await _internalPlayer.Pause();
    }

    public async Task Play(string id)
    {
        await InitialisePlayer();

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

    public async Task InitialisePlayer()
    {
        if (await IsDeviceActive())
            return;

        if (await InitialiseNewDevice())
            return;

        if (await IsDeviceActive())
            return;

        throw new Exception("No Device ID received - Spotify player not ready");
    }

    private async Task<bool> InitialiseNewDevice()
    {
        var storedAccessToken = await _storeGetter.GetValue<string>(StoreKey.SpotifyAccessToken);
        var accessToken = storedAccessToken.Value;

        var connected = await _interop.ConnectPlayer(accessToken);
        if (!connected)
            throw new Exception("Failed to connect to Spotify player");

        var deviceId = await _storeGetter.AwaitValue<string>(StoreKey.SpotifyDeviceId, 60, CancellationToken.None);

        return deviceId != null;
    }

    private async Task<bool> IsDeviceActive()
    {
        var devices = await _player.GetDevices();

        var device = devices?.Devices.SingleOrDefault(d => d.name == "Cadenza");
        if (device != null)
        {
            await _storeSetter.SetValue(StoreKey.SpotifyDeviceId, device.id);
            return true;
        }

        return false;
    }
}