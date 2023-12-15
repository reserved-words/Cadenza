using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record UpdateRecentPlayHistoryRequest(PlayStatus Status, TrackFullVM Track, int SecondsPlayed);
public record UpdateRecentPlayHistoryResult();
