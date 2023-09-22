namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record TrackRemovalState(bool IsLoading, int TrackId, bool Succeeded)
{
    private static TrackRemovalState Init() => new TrackRemovalState(false, 0, false);
}
