using Cadenza.Common.Domain.Model.Album;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewAlbumState(bool IsLoading, AlbumInfo Album, List<Disc> Discs) 
{
    private static ViewAlbumState Init() => new ViewAlbumState(true, null, null);
}