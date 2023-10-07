namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewGenreState(bool IsLoading, string Genre, List<ArtistVM> Artists) 
{
    private static ViewGenreState Init() => new ViewGenreState(true, null, null);
}