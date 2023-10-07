namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewArtistState(bool IsLoading, ArtistDetailsVM Artist, List<ArtistReleaseGroupVM> Releases) 
{
    private static ViewArtistState Init() => new ViewArtistState(true, null, null);
}