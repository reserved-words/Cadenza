namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewTrackState(bool IsLoading, TrackFullVM Track) 
{
    private static ViewTrackState Init() => new ViewTrackState(true, null);
}