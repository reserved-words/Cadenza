using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.History;

namespace Cadenza.State.Actions;

public record FetchPlayHistoryAlbumsRequest(HistoryPeriod Period);
public record FetchPlayHistoryArtistsRequest(HistoryPeriod Period);
public record FetchPlayHistoryTracksRequest(HistoryPeriod Period);

public record FetchPlayHistoryAlbumsResult(HistoryPeriod Period, List<PlayedAlbum> Result);
public record FetchPlayHistoryArtistsResult(HistoryPeriod Period, List<PlayedArtist> Result);
public record FetchPlayHistoryTracksResult(HistoryPeriod Period, List<PlayedTrack> Result);
