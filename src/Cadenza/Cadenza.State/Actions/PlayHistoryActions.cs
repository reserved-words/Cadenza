using Cadenza.Common.Domain.Enums;

namespace Cadenza.State.Actions;

public record FetchPlayHistoryAlbumsRequest(HistoryPeriod Period);
public record FetchPlayHistoryArtistsRequest(HistoryPeriod Period);
public record FetchPlayHistoryTracksRequest(HistoryPeriod Period);

public record FetchPlayHistoryAlbumsResult(HistoryPeriod Period, List<PlayedAlbumVM> Result);
public record FetchPlayHistoryArtistsResult(HistoryPeriod Period, List<PlayedArtistVM> Result);
public record FetchPlayHistoryTracksResult(HistoryPeriod Period, List<PlayedTrackVM> Result);
