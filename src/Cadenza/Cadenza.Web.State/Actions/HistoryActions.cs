using Cadenza.Common.Enums;
using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record FetchRecentlyAddedAlbumsRequest();
public record FetchRecentlyPlayedTracksRequest();
public record FetchRecentlyPlayedAlbumsRequest();
public record FetchTopPlayedAlbumsRequest(HistoryPeriod Period);
public record FetchTopPlayedArtistsRequest(HistoryPeriod Period);


public record FetchRecentlyAddedAlbumsResult(List<RecentAlbumVM> Result);
public record FetchRecentlyPlayedTracksResult(List<RecentTrackVM> Result);
public record FetchRecentlyPlayedAlbumsResult(IReadOnlyCollection<RecentAlbumVM> Result);
public record FetchTopPlayedAlbumsResult(HistoryPeriod Period, IReadOnlyCollection<TopAlbumVM> Result);
public record FetchTopPlayedArtistsResult(HistoryPeriod Period, IReadOnlyCollection<TopArtistVM> Result);
