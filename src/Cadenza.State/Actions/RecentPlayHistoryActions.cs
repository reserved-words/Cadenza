using Cadenza.Common.Domain.Model.History;

namespace Cadenza.State.Actions;

public record FetchRecentPlayHistoryAction();

public record FetchRecentPlayHistoryResultAction(List<RecentTrack> Result);
