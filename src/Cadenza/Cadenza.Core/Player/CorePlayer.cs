﻿namespace Cadenza.Core;

public class CorePlayer : IPlayer
{
    private readonly IStoreGetter _storeGetter;
    private readonly IStoreSetter _storeSetter;
    private readonly Dictionary<LibrarySource, IAudioPlayer> _services;

    public CorePlayer(IStoreGetter store, IEnumerable<ISourcePlayer> services, IStoreSetter storeSetter)
    {
        _storeGetter = store;
        _storeSetter = storeSetter;
        _services = services.ToDictionary(s => s.Source, s => s as IAudioPlayer);
    }

    public async Task<TrackProgress> Play(BasicTrack track)
    {
        await _storeSetter.SetValue(StoreKey.CurrentTrackSource, track.Source);

        var service = await GetCurrentService();
        return await service.Play(track.Id);
    }

    public async Task<TrackProgress> Pause()
    {
        var service = await GetCurrentService();
        return await service.Pause();
    }

    public async Task<TrackProgress> Stop()
    {
        var service = await GetCurrentService();

        if (service == null)
            return (new TrackProgress(0, 0));

        return await service.Stop();
    }

    public async Task<TrackProgress> Resume()
    {
        var service = await GetCurrentService();
        return await service.Resume();
    }

    private async Task<IAudioPlayer> GetCurrentService()
    {
        var source = await _storeGetter.GetValue(StoreKey.CurrentTrackSource);

        if (source == null)
            return null;

        return _services[Enum.Parse<LibrarySource>(source)];
    }
}