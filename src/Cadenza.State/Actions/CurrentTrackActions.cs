using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.State.Actions;

public record UpdateCurrentTrackRequest(int TrackId);

public record UpdateCurrentTrackAction(TrackFull Result);
