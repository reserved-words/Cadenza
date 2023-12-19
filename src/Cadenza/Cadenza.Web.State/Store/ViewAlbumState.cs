using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewAlbumState(bool IsLoading, AlbumFullVM Album)
{
    private static ViewAlbumState Init() => new ViewAlbumState(true, null);
}