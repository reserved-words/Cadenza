using Cadenza.Common.Domain.Model.History;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record RecentPlayHistoryState(bool IsLoading, List<RecentTrack> Tracks) 
{
    private static RecentPlayHistoryState Init() => new RecentPlayHistoryState(true, null);
}
