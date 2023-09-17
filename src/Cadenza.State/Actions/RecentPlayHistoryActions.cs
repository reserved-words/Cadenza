using Cadenza.Common.Domain.Model.History;

namespace Cadenza.State.Actions;

public record UpdateRecentPlayHistoryRequest();

public record UpdateRecentPlayHistoryResult();

public record FetchRecentPlayHistoryRequest();

public record FetchRecentPlayHistoryResult(List<RecentTrack> Result);
