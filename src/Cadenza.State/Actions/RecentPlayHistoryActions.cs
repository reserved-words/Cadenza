using Cadenza.Common.Domain.Model.History;
using Cadenza.Common.Domain.Model.Track;
using Cadenza.Web.Common.Enums;

namespace Cadenza.State.Actions;

public record UpdateRecentPlayHistoryRequest(PlayStatus Status, TrackFull Track, int SecondsPlayed);

public record UpdateRecentPlayHistoryResult();

public record FetchRecentPlayHistoryRequest();

public record FetchRecentPlayHistoryResult(List<RecentTrack> Result);
