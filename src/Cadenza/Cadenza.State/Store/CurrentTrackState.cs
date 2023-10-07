namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record CurrentTrackState(bool IsLoading, TrackFullVM Track, bool IsLastInPlaylist) 
{
    private static CurrentTrackState Init() => new CurrentTrackState(false, null, true);
}