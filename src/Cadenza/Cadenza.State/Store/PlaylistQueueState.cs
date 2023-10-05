namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlaylistQueueState(bool IsLoading, Stack<int> Played, Stack<int> ToPlay, int CurrentTrack)
{
    public static PlaylistQueueState Init() => new PlaylistQueueState(false, new Stack<int>(), new Stack<int>(), 0);

    public bool IsCurrentTrackLast => ToPlay.Count == 0;
}