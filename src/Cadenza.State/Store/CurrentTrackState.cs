using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record CurrentTrackState(bool IsLoading, TrackFull Track, bool IsLastInPlaylist) 
{
    private static CurrentTrackState Init() => new CurrentTrackState(false, null, true);
}