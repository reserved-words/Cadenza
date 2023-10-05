using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record FetchViewTrackRequest(int TrackId);

public record FetchViewTrackResult(TrackFull Track);
