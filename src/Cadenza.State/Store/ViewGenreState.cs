using Cadenza.Common.Domain.Model.Artist;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewGenreState(bool IsLoading, string Genre, List<Artist> Artists) 
{
    private static ViewGenreState Init() => new ViewGenreState(true, null, null);
}