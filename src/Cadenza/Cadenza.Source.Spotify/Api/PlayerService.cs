using Cadenza.Core.Model;
using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Interfaces;

namespace Cadenza.Source.Spotify.Api;

internal class PlayerService : IPlayerService
{
    private readonly IPlayerApi _api;
    private readonly IDeviceHelper _devicesApi;
    private readonly IProgressService _progressApi;

    public PlayerService(IPlayerApi api, IDeviceHelper devicesApi, IProgressService progressApi)
    {
        _api = api;
        _devicesApi = devicesApi;
        _progressApi = progressApi;
    }

    public async Task<TrackProgress> Stop()
    {
        var deviceId = await _devicesApi.GetDeviceId(false);
        await _api.Pause(deviceId);
        return await _progressApi.GetProgress();
    }

    public async Task Play(string trackId)
    {
        var deviceId = await _devicesApi.GetDeviceId(false);
        await _api.Play(deviceId, trackId);
    }

    public async Task<TrackProgress> Pause()
    {
        var deviceId = await _devicesApi.GetDeviceId(false);
        await _api.Pause(deviceId);
        return await _progressApi.GetProgress();
    }

    public async Task<TrackProgress> Resume()
    {
        var deviceId = await _devicesApi.GetDeviceId(false);
        await _api.Play(deviceId);
        return await _progressApi.GetProgress();
    }
}
