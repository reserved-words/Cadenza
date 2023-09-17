using Cadenza.Common.Domain.Model.History;

namespace Cadenza.State.Actions;

public record FetchRecentPlayHistoryRequest();

public record FetchRecentPlayHistoryAction(List<RecentTrack> Result);
