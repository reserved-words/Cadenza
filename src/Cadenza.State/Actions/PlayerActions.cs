using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.State.Actions;

public record PlayerPlayRequest(TrackFull Track);
public record PlayerPauseRequest(TrackFull Track);
public record PlayerResumeRequest(TrackFull Track);
public record PlayerStopRequest(TrackFull Track);
