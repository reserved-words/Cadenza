using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record EditArtistState(bool IsLoading, ArtistDetailsVM Artist, IReadOnlyCollection<AlbumVM> Releases)
{
    private static EditArtistState Init() => new EditArtistState(true, null, null);
}