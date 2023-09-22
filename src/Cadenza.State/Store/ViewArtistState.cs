using Cadenza.Common.Domain.Model.Artist;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewArtistState(bool IsLoading, ArtistInfo Artist, List<ArtistReleaseGroup> Releases) 
{
    private static ViewArtistState Init() => new ViewArtistState(true, null, null);
}