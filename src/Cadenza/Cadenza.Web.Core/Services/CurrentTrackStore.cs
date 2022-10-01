namespace Cadenza.Web.Core.Services;

public class CurrentTrackStore : ICurrentTrackStore
{
    private readonly IAppStore _store;

    public CurrentTrackStore(IAppStore store)
    {
        _store = store;
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
}