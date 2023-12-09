using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewArtistState(bool IsLoading, ArtistDetailsVM Artist, IReadOnlyCollection<ArtistReleaseGroupVM> Releases)
{
    private static ViewArtistState Init() => new ViewArtistState(true, null, null);
}
