using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ViewArtistState(bool IsLoading, ArtistDetails Artist, List<ArtistReleaseGroup> Releases) 
{
    private static ViewArtistState Init() => new ViewArtistState(true, null, null);
}