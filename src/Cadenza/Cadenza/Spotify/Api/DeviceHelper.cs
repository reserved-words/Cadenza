namespace Cadenza.Source.Spotify.Player;

public class DeviceHelper : IDeviceHelper
{
    private readonly IStoreGetter _storeGetter;
    private readonly IStoreSetter _storeSetter;
    private readonly ISpotifyInterop _interop;
    private readonly IDevicesApi _api;

    public DeviceHelper(ISpotifyInterop interop, IStoreGetter storeGetter, IStoreSetter storeSetter, IDevicesApi api)
    {
        _interop = interop;
        _storeGetter = storeGetter;
        _storeSetter = storeSetter;
        _api = api;
    }

    public async Task<string> GetDeviceId(string accessToken, bool forceCreateNew)
    {
        var deviceId = forceCreateNew
            ? null
            : await GetCurrentDeviceId(accessToken);

        if (deviceId == null)
        {
            deviceId = await GetNewDeviceId(accessToken);
        }

        if (deviceId == null)
        {
            deviceId = await GetCurrentDeviceId(accessToken);
        }

        if (deviceId == null)
            throw new Exception("No Device ID received - Spotify player not ready");

        return deviceId;
    }

    private async Task<string> GetNewDeviceId(string accessToken)
    {
        var connected = await _interop.ConnectPlayer(accessToken);
        if (!connected)
            throw new Exception("Failed to connect to Spotify player");

        var deviceId = await _storeGetter.AwaitValue<string>(StoreKey.SpotifyDeviceId, 60, CancellationToken.None);

        return deviceId?.Value;
    }

    private async Task<string> GetCurrentDeviceId(string accessToken)
    {
        var devices = await _api.GetDevices(accessToken);

        var device = devices?.Devices.SingleOrDefault(d => d.name == "Cadenza");
        if (device != null)
        {
            await _storeSetter.SetValue(StoreKey.SpotifyDeviceId, device.id);
            return device.id;
        }

        return null;
    }
}
