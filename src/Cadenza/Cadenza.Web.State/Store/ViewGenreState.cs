using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewGenreState(bool IsLoading, string Genre, IReadOnlyCollection<ArtistVM> Artists)
{
    private static ViewGenreState Init() => new ViewGenreState(true, null, null);
}