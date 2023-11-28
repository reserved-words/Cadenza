using Cadenza.Common.Enums;

namespace Cadenza.Web.State.Actions;

public record FetchPlayHistoryAlbumsRequest(HistoryPeriod Period);
public record FetchPlayHistoryArtistsRequest(HistoryPeriod Period);
public record FetchPlayHistoryTracksRequest(HistoryPeriod Period);

public record FetchPlayHistoryAlbumsResult(HistoryPeriod Period, IReadOnlyCollection<TopAlbumVM> Result);
public record FetchPlayHistoryArtistsResult(HistoryPeriod Period, IReadOnlyCollection<TopArtistVM> Result);
public record FetchPlayHistoryTracksResult(HistoryPeriod Period, IReadOnlyCollection<TopTrackVM> Result);
