namespace Cadenza.State.Actions;

public record UpdateRecentPlayHistoryRequest(PlayStatus Status, TrackFullVM Track, int SecondsPlayed);

public record UpdateRecentPlayHistoryResult();

public record FetchRecentPlayHistoryRequest();

public record FetchRecentPlayHistoryResult(List<RecentTrack> Result);
