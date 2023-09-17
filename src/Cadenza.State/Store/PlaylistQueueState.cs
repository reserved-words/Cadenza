using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlaylistQueueState(bool IsLoading, Stack<PlayTrack> Played, Stack<PlayTrack> ToPlay, PlayTrack CurrentTrack)
{
    private static PlaylistQueueState Init() => new PlaylistQueueState(false, new Stack<PlayTrack>(), new Stack<PlayTrack>(), null);

    public bool IsCurrentTrackLast => ToPlay.Count == 0;
}