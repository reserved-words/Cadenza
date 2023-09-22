using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.State.Actions;

public record UpdateRecentPlayHistoryRequest(PlayStatus Status, TrackFull Track, int SecondsPlayed);

public record UpdateRecentPlayHistoryResult();

public record FetchRecentPlayHistoryRequest();

public record FetchRecentPlayHistoryResult(List<RecentTrack> Result);
