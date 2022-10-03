using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.Core.Services;

public class CurrentTrackStore : ICurrentTrackStore
{
    private readonly IAppStore _store;
    private readonly ITrackRepository _repository;

    public CurrentTrackStore(IAppStore store, ITrackRepository repository)
    {
        _store = store;
        _repository = repository;
    }

    public async Task<LibrarySource?> GetCurrentSource()
    {
        var storedSource = await _store.GetValue<LibrarySource?>(StoreKey.CurrentTrackSource);
        if (storedSource == null)
            return null;

        return storedSource.Value;
    }

    public async Task<TrackFull> GetCurrentTrack()
    {
        var track = await _store.GetValue<TrackFull>(StoreKey.CurrentTrack);
        if (track == null)
            return null;

        return track.Value;
    }

    public async Task SetCurrentTrack(string id)
    {
        var track = await _repository.GetTrack(id);
        await _store.SetValue(StoreKey.CurrentTrack, track);
        await _store.SetValue(StoreKey.CurrentTrackSource, track.Track.Source);
    }
}