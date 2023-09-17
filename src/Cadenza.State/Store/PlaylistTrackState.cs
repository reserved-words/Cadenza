using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlaylistTrackState(bool IsLoading, PlayTrack CurrentTrack, bool IsCurrentTrackLast)
{
    private static PlaylistTrackState Init() => new PlaylistTrackState(false, null, true);
}