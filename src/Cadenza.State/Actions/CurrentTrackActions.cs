using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.State.Actions;

public record FetchCurrentTrackAction(int TrackId);

public record FetchCurrentTrackResultAction(TrackFull Result);
