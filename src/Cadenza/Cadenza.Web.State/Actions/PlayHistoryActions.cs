using Cadenza.Common.Enums;

namespace Cadenza.Web.State.Actions;

public record FetchPlayHistoryAlbumsRequest(HistoryPeriod Period);
public record FetchPlayHistoryArtistsRequest(HistoryPeriod Period);
public record FetchPlayHistoryTracksRequest(HistoryPeriod Period);

public record FetchPlayHistoryAlbumsResult(HistoryPeriod Period, IReadOnlyCollection<PlayedAlbumVM> Result);
public record FetchPlayHistoryArtistsResult(HistoryPeriod Period, IReadOnlyCollection<PlayedArtistVM> Result);
public record FetchPlayHistoryTracksResult(HistoryPeriod Period, IReadOnlyCollection<PlayedTrackVM> Result);
