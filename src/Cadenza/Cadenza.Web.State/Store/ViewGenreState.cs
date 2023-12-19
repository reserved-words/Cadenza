using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewGenreState(bool IsLoading, GenreFullVM Genre)
{
    private static ViewGenreState Init() => new ViewGenreState(true, null);
}