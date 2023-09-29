using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewAlbumState(bool IsLoading, AlbumInfo Album, List<Disc> Discs) 
{
    private static ViewAlbumState Init() => new ViewAlbumState(true, null, null);
}