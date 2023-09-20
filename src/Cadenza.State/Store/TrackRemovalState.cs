namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record TrackRemovalState(bool IsLoading, int LastTrackRemovedId, string Error)
{
    private static TrackRemovalState Init() => new TrackRemovalState(false, 0, null);
}
