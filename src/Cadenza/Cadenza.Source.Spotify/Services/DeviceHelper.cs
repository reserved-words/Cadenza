using Cadenza.Core.App;
using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Interfaces;

namespace Cadenza.Source.Spotify.Services;

internal class DeviceHelper : IDeviceHelper
{
    private readonly IStoreGetter _storeGetter;
    private readonly IStoreSetter _storeSetter;
    private readonly ISpotifyInterop _interop;
    private readonly IDevicesApi _api;
    private readonly ITokenProvider _tokenProvider;

    public DeviceHelper(ISpotifyInterop interop, IStoreGetter storeGetter, IStoreSetter storeSetter, IDevicesApi api, ITokenProvider tokenProvider)
    {
        _interop = interop;
        _storeGetter = storeGetter;
        _storeSetter = storeSetter;
        _api = api;
        _tokenProvider = tokenProvider;
    }

    public async Task<string> GetDeviceId(bool forceCreateNew)
    {
        var deviceId = forceCreateNew
            ? null
            : await GetCurrentDeviceId();

        if (deviceId == null)
        {
            deviceId = await GetNewDeviceId();
        }

        if (deviceId == null)
        {
            deviceId = await GetCurrentDeviceId();
        }

        if (deviceId == null)
            throw new Exception("No Device ID received - Spotify player not ready");

        return deviceId;
    }

    private async Task<string> GetNewDeviceId()
    {
        var accessToken = await _tokenProvider.GetAccessToken(false);

        var connected = await _interop.ConnectPlayer(accessToken);
        if (!connected)
            throw new Exception("Failed to connect to Spotify player");

        var deviceId = await _storeGetter.AwaitValue<string>(StoreKey.SpotifyDeviceId, 60, CancellationToken.None);

        return deviceId?.Value;
    }

    private async Task<string> GetCurrentDeviceId()
    {
        var devices = await _api.GetDevices();

        var device = devices?.Devices.FirstOrDefault(d => d.name == "Cadenza");
        if (device != null)
        {
            await _storeSetter.SetValue(StoreKey.SpotifyDeviceId, device.id);
            return device.id;
        }

        return null;
    }
}
