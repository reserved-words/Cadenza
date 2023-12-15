using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record UpdateRecentPlayHistoryRequest(PlayStatus Status, TrackFullVM Track, int SecondsPlayed);
public record UpdateRecentPlayHistoryResult();

public record FetchRecentPlayHistoryRequest();
public record FetchRecentPlayHistoryResult(List<RecentTrackVM> Result);

public record FetchRecentlyAddedAlbumsRequest();
public record FetchRecentlyAddedAlbumsResult(List<RecentAlbumVM> Result);
